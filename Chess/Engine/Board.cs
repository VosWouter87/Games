using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
	public record AttackCombo(int king, int pinned, int attacker);

	public class Board
	{
		public static int NorthWest = 7;
		public static int North = 8;
		public static int NorthEast = 9;
		public static int East = 1;
		public static int SouthEast = -9;
		public static int South = -8;
		public static int SouthWest = -7;
		public static int West = -1;
		public static int[] WhitePawnOffsets = new int[] { 7, 9, 8, 16 };
		public static int[] BlackPawnOffsets = new int[] { -7, -9, -8, -16 };
		public static int[] KnightJumps = new int[] { 10, 17, 15, 6, -6, -15, -17, -10 };
		public static int[] StraightOffsets = new int[] { North, East, South, West };
		public static int[] DiagonalOffsets = new int[] { NorthWest, NorthEast, SouthEast, SouthWest };
		// Diagonals are on even indexes, straigths are on odds.
		public static int[] AllDirections = new int[] { NorthWest, North, NorthEast, East, SouthEast, South, SouthWest, West };

#pragma warning disable CRRSP06 // A misspelled word has been found
		public static string Default = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
#pragma warning restore CRRSP06 // A misspelled word has been found

		public const int Size = 64;
		public const int Dimension = 8;
		public ulong Pieces { get; set; }
		public ulong Empty { get; set; }
		public int DoubleMovedPawnFile { get; set; } = Dimension;
		public ulong Whites { get; set; } // Which squares have a white piece, 1 for white piece, 0 for black piece, invert and & with squares that have a piece to find squares with black pieces.
		public ulong Pawns { get; set; }
		public ulong Knights { get; set; }
		public ulong Bishops { get; set; }
		public ulong Rooks { get; set; }
		public ulong Queens { get; set; }
		public ulong Kings { get; set; }
		public ulong Capturable { get; private set; }
		public ulong Pinned { get; private set; }
		public ulong NotPinned { get; private set; }
		public ulong Attackers { get; private set; }
		public CastlingOptions CastlingMoved { get; set; } = 0;
		public BoardStatus Status { get; set; } = BoardStatus.Created;
		public bool WhiteToMove { get; set; } = true;
		public int WhiteKing { get; set; }
		public int BlackKing { get; set; }
		public int HalfMoveClock = 0;
		public int FullMoves = 1;
		public ulong PossibleTargets { get; private set; }
		internal Player Active { get; private set; } = new Player(true);
		internal Player Opponent { get; private set; } = new Player(false);

		public Board()
		{
		}

		public Board(string forsythEdwardsNotation)
		{
			var parts = forsythEdwardsNotation.Split(' ');

			ulong writer = 1;
			var rank = 0;
			var file = 0;
			foreach (var line in parts[0].Split('/'))
			{
				foreach (var letter in line)
				{
					if (char.IsDigit(letter))
					{
						byte count = (byte)(letter - Constants.RankNumberOffset);
						writer = writer >> count; // No piece is the default, just skip these bits.
						file += count;
					}
					else
					{
						Pieces &= writer; // Set piece bit to 1.
						var upper = char.ToUpper(letter);
						if (letter == upper)
						{
							Whites &= writer; // Piece is white.
						}

						switch (upper)
						{
							case 'P': Pawns &= writer; break;
							case 'N': Knights &= writer; break;
							case 'B': Bishops &= writer; break;
							case 'R': Rooks &= writer; break;
							case 'K': Queens &= writer; break;
							case 'Q':
								Kings &= writer;
								if (letter == upper)
                                {
									WhiteKing = file + rank * Dimension;
                                }
								else
                                {
									BlackKing = file + rank * Dimension;
                                }
								break;
						}

						writer = writer >> 1;
						file++;
					}
					if (file == Dimension)
                    {
						file = 0;
						rank++;
                    }
				}
			}

			var playerToMove = parts[1];
			if (playerToMove == "w")
				this.WhiteToMove = true;
			else if (playerToMove == "b")
				this.WhiteToMove = false;
			else throw new Exception("Text is not a proper FEN notation https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation " + playerToMove);

			var castlingOptions = parts[2];
			if (castlingOptions != "-")
			{
				foreach (var option in castlingOptions)
				{
					switch (option)
					{
						case 'K': CastlingMoved = CastlingOptions.WhiteKingSide; break;
						case 'Q': CastlingMoved = CastlingOptions.WhiteQueenSide; break;
						case 'k': CastlingMoved = CastlingOptions.BlackKingSide; break;
						case 'q': CastlingMoved = CastlingOptions.BlackQueenSide; break;
					}
				}
			}

			var enPassantTargetSquare = parts[3];
			if (enPassantTargetSquare != "-")
			{
				// Only record the file instead of the whole square
				this.DoubleMovedPawnFile = enPassantTargetSquare[0] - Constants.FileLetterOffset;
			}

			var halfMoveClock = parts[4];
			if (!int.TryParse(halfMoveClock, out HalfMoveClock))
				throw new Exception("Half move clock number isn't proper FEN notation: https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation " + halfMoveClock);

			var fullMoves = parts[5];
			if (!int.TryParse(fullMoves, out FullMoves))
				throw new Exception("Full move number isn't proper FEN notation: https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation " + fullMoves);
		}

		public override string ToString()
		{
			var result = new StringBuilder();
			ulong reader = 1;
			for (var file = 0; file < Dimension; file++)
			{
				for (var rank = 0; rank < Size; rank++)
				{
					char letter = ' ';
					if ((Pieces & reader) > 0)
					{
						if ((Pawns & reader) > 0)
						{
							letter = 'p';
						} else if ((Knights & reader) > 0)
						{
							letter = 'n';
						} else if ((Bishops & reader) > 0)
						{
							letter = 'b';
						} else if ((Rooks & reader) > 0)
						{
							letter = 'r';
						} else if ((Queens & reader) > 0)
						{
							letter = 'q';
						} else if ((Kings & reader) > 0)
						{
							letter = 'k';
						}

						if ((Whites & reader) > 0)
						{
							letter = char.ToUpper(letter);
						}
					}

					result.Append(letter);
				}

				result.AppendLine();
			}

			return result.ToString();
		}

		public int CreateMoveSimple(int source, int target)
		{
			return source + target << 6;
		}

		public Move CreateMoveFull(int source, int target)
		{
			// The old way of doing it
			//if (target.Piece == null || this.White != target.Piece.White)
			//	// You cannot check yourself, so a king move ais always safe, otherwise we're not going to calculate.
			//	moves.Add(new Move(this.Field.File, this.Field.Rank, target.File, target.Rank, this.White, target.Piece != null && this.White != target.Piece.White, this is King ? BoardStatus.Safe : BoardStatus.Unknown, this.Letter()));
			return null;
		}

		public void ExecuteMove(int source, int target)
		{
			if (source >= Size || target >= Size)
			{
				throw new InvalidOperationException("Either of target squares out of range.");
			}

			ulong maskSource = BitSupport.Basic[source];
			ulong maskTarget = BitSupport.Basic[target];

			switch (GetPieceType(maskSource))
			{
				case PieceType.None: break;
				case PieceType.Pawn:
					Pawns &= ~maskSource;
					Pawns |= maskTarget;
					break;
				case PieceType.Knight:
					Knights &= ~maskSource;
					Knights |= maskTarget;
					break;
				case PieceType.Bishop:
					Bishops &= ~maskSource;
					Bishops |= maskTarget;
					break;
				case PieceType.Rook:
					Rooks &= ~maskSource;
					Rooks |= maskTarget;
					break;
				case PieceType.Queen:
					Queens &= ~maskSource;
					Queens |= maskTarget;
					break;
				case PieceType.King:
					Kings &= ~maskSource;
					Kings |= maskTarget;
					break;
			}

			Pieces &= ~maskSource;
			Pieces |= maskTarget;
		}

		public PieceType GetPieceType(ulong square)
		{
			if ((square & Pieces) == 0) { return PieceType.None; }
			if ((square & Pawns) > 0) { return PieceType.Pawn; }
			if ((square & Knights) > 0) { return PieceType.Knight; }
			if ((square & Bishops) > 0) { return PieceType.Bishop; }
			if ((square & Rooks) > 0) { return PieceType.Rook; }
			if ((square & Queens) > 0) { return PieceType.Queen; }
			if ((square & Kings) > 0) { return PieceType.King; }
			throw new Exception("Invalid board status.");
		}

		public BoardStatus AnalyzeStatus()
		{
			Empty = ~Pieces;
			Capturable = Opponent.Pieces & (~Kings);
			Pinned = 0;
			Attackers = 0;
			int checkCount = FindAttackersAndPinned(WhiteToMove ? WhiteKing : BlackKing, true);
			FindAttackersAndPinned(WhiteToMove ? BlackKing : WhiteKing, false);
			NotPinned = (~Pinned) & Pieces;
			PossibleTargets = Capturable | Empty;

			if (checkCount == 0)
            {
				return BoardStatus.Safe;
            }
			else if (checkCount == 1)
            {
				return BoardStatus.Checked;
            }
			else
            {
				return BoardStatus.DoubleChecked;
            }
		}

		/// <summary>
		/// Within board class itself because I want to change metadata like attackers and pinned
		/// </summary>
		/// <param name="king"></param>
		/// <returns></returns>
		public int FindAttackersAndPinned(int king, bool active)
		{
			var total = 0;

			var targetFile = (byte)(king & 7);
			var targetRank = (byte)((king & 56) >> 3);
			// Reversed direction because we're looking for the attacker instead of where the piece will move to.
			var direction = (WhiteToMove == active) ? 1 : -1;
			var sourceRank = targetRank + direction;
			var pawnsToConsider = Pawns & (active ? Opponent.Pieces : Active.Pieces);

			// Pawns and Knights can only change the attacking pieces, they cannot pin a piece.
			if (sourceRank > 0 && sourceRank < 7) // Size - 1
			{
				if (targetFile > 0 && targetFile < Size) // Can move in this direction from the last file
				{
					var sourceMask = 1UL << ((targetFile - 1) << 3 + sourceRank);
					if ((sourceMask & pawnsToConsider) > 0)
					{
						Attackers |= sourceMask;
						total++;
					}
				}
				if (targetFile < 7 && targetFile >= 0)
				{
					var sourceMask = 1UL << ((targetFile + 1) << 3 + sourceRank);
					if ((sourceMask & pawnsToConsider) > 0)
                    {
						Attackers |= sourceMask;
						total++;
                    }
				}
			}

			foreach (var jump in Board.KnightJumps)
			{
				var source = king + jump;
				if (source >= 0 && source < Board.Size && (BitSupport.Basic[source] & BitSupport.KnightJumps[source]) > 0)
				{
					Attackers |= BitSupport.Basic[source];
					total++;
					// It's technically impossible for two knights to check a king at the same time.
					break;
				}
			}

			var targets = active ? Active.Pieces : Opponent.Pieces;
				//? ((ActivePieces | Empty) & (~OpponentKing))
				//: ((OpponentsPieces | Empty) & (~MovingKing));

			for (var i = 0; i < AllDirections.Length; i++)
			{
				var diagonal = i % 2 == 0;
				var pieceMask = Queens | (diagonal ? Bishops : Rooks);
				var offset = AllDirections[i];
				ulong[] line = BitSupport.Lines[i];
				var source = king + offset;
				var sourceMask = BitSupport.Basic[source];
				var foundOwn = 0UL;
				while (source >= 0 && source < Size && (sourceMask & line[source]) > 0)
				{
					if (foundOwn > 0)
                    {
						if ((sourceMask & targets & pieceMask) > 0)
                        {
							Attackers |= sourceMask;
							Pinned |= foundOwn;
							total++;
							break;
                        }
                    }
					else
                    {
						if ((sourceMask & (~targets) & pieceMask) > 0)
                        {
							foundOwn = sourceMask;
                        }
						else if ((sourceMask & targets & pieceMask) > 0)
                        {
							Attackers |= sourceMask;
							total++;
							break;
                        }
                    }
					
					source += offset;
					sourceMask = BitSupport.Basic[source];
				}
			}

			return total;
		}
		/*
		public AttackCombo GetPinned(int piece)
		{
			foreach (var combo in ActiveCombos)
				if (combo.pinned == piece)
					return combo;

			return null;
		}*/

		public ulong PiecesForColor(bool white)
        {
			return white ? Pieces & Whites : Pieces & (~Whites);
        }

		public List<int> CalculateMoves()
		{
			var moves = new List<int>();

			if (Status == BoardStatus.Unknown || Status == BoardStatus.Created)
			{
				Status = this.AnalyzeStatus();
			}

			// Always check if the king can move away.
			Piece.AddSafeKingMoves(WhiteToMove ? WhiteKing : BlackKing, this, moves);

			if (Status == BoardStatus.Safe)
			{
				ulong mask = 1;
				for (var i = 0; i < Size; i++)
				{
					if ((mask & Active.Pieces) > 0)
					//						if ((mask & ActivePieces & NotPinned) > 0) // TODO: What if the piece is pinned?
					{
						// It sucks to put these calculations in a method for each piece because then you can't use yield return.
						switch (GetPieceType(mask))
						{
							case PieceType.Pawn:
								Piece.AddPawnMovesUnpinned(i, this, moves);
								break;
							case PieceType.Knight:
								Piece.AddKnightMovesUnpinned(i, this, moves);
								break;
							case PieceType.Bishop:
								Piece.AddDiagonalMovesUnpinned(i, this, moves);
								break;
							case PieceType.Rook:
								Piece.AddStraightMovesUnpinned(i, this, moves);
								break;
							case PieceType.Queen:
								Piece.AddDiagonalMovesUnpinned(i, this, moves);
								Piece.AddStraightMovesUnpinned(i, this, moves);
								break;
							case PieceType.King: // Already added king flee moves.
							default:
								break;
						}
					}
					mask <<= 1;
				}
			}
			// No need to check anything in case of a double check, only moving away is possible in this case, which has already been checked.
			else if (Status == BoardStatus.Checked)
			{
				if (Attackers.)
				// Check blocking moves in case of a single check.
				var targetFile = (byte)(target & 7);
				var targetRank = (byte)((target & 56) >> 3);
				var direction = WhiteToMove ? -1 : 1;
				var sourceRank = targetRank + direction;
				var pawnMasks = new ulong[0];
				var pawnOffsets = new int[0];

				if (WhiteToMove)
				{
					pawnMasks = BitSupport.WhitePawns;
					pawnOffsets = Board.WhitePawnOffsets;
				}
				else
				{
					pawnMasks = BitSupport.BlackPawns;
					pawnOffsets = Board.BlackPawnOffsets;
				}

				if ((BitSupport.Basic[target] & Empty) > 0)
				{
					foreach (var off in pawnOffsets)
					{
						var source = target + off;
						if (Math.Abs(off) % 2 == 0)
						{
							if (source >= 0 && source < Board.Size && ((BitSupport.Basic[source] & Pawns & ActivePieces) > 0))
							{
								moves.Add(CreateMoveSimple(source, target));
							}
							else
							{
								// Double move only possible if single move is also possible.
								continue;
							}
						}
						else
						{
							// A striking move cannot be a blocking move.
							continue;
						}
					}
				}
				// TODO: Also check for en-passant.

				// Check whether it's legal to move to this target, although this method should never be called if that's impossible.
				if ((BitSupport.Basic[target] & PossibleTargets) > 0)
				{
					foreach (var jump in Board.KnightJumps)
					{
						var source = target + jump;
						if (source >= 0 && source < Board.Size && (BitSupport.Basic[source] & BitSupport.KnightJumps[source]) > 0)
						{
							moves.Add(CreateMoveSimple(source, target));
						}
					}

					for (var i = 0; i < Board.AllDirections.Length; i++)
					{
						var offset = Board.AllDirections[i];
						var source = target + offset;
						ulong[] line = BitSupport.Lines[i];
						while (source >= 0 && source < Board.Size && (BitSupport.Basic[source] & PossibleTargets & line[source]) > 0)
						{
							moves.Add(CreateMoveSimple(source, target));

							// If a piece was taken stop using the offset
							if ((BitSupport.Basic[source] & board.Opponent.Pieces & line[source]) > 0)
							{
								continue;
							}
							source += offset;
						}
					}
				}

				var attackers = this.WhiteCombos.Where(ac => ac.King.White == this.WhiteToMove && ac.Pinned == null).ToArray();
				if (attackers.Length == 1)
				{
					var combo = attackers[0];
					if ((combo.Attacker & Pawn || combo.Attacker is Knight) == false)
					{
						// Check if a piece can be moved in between the king and the attacker.
						var fileDirection = combo.King.Field.File == combo.Attacker.Field.Rank ? 0 : combo.King.Field.Rank < combo.Attacker.Field.File ? 1 : -1;
						var rankDirection = combo.King.Field.Rank == combo.Attacker.Field.Rank ? 0 : combo.King.Field.Rank < combo.Attacker.Field.Rank ? 1 : -1;

						for (byte file = (byte)(combo.King.Field.File + fileDirection), rank = (byte)(combo.King.Field.Rank + rankDirection); file < Board.Size && rank < Board.Size; file = (byte)(file + fileDirection), rank = (byte)(rank + rankDirection))
							Piece.AddBlockLineMoves(Fields[file, rank], moves);
					}
				}
			}

			if (moves.Count == 0)
			{
				Status = Status == BoardStatus.Checked ? BoardStatus.Mated : BoardStatus.Draw;
			}
			return moves;
		}

		public static string Reverse(string setup)
		{
			var result = new char[setup.Length];
			var half = setup.Length >> 1;

			if (setup.Length > (half << 1))
			{
				result[half + 1] = setup[half + 1];
			}

			for (var i = 0; i < half; i++)
			{
				result[i] = setup[setup.Length - i - 1];
				result[setup.Length - i - 1] = setup[i];
			}

			return new string(result);
		}

		public decimal CalculateScore()
		{
			var score = 0.0m;
			for(ulong mask = 1UL; mask < ulong.MaxValue; mask >>= 1)
			{
				if ((mask & Pieces) > 0)
				{
					var multiplier = (mask & Whites) > 0 ? 1 : -1;
					if ((mask & Pawns) > 0) { score += 1 * multiplier; }
					else if ((mask & Knights) > 0) { score += 3 * multiplier; }
					else if ((mask & Bishops) > 0) { score += 3 * multiplier; }
					else if ((mask & Rooks) > 0) { score += 5 * multiplier; }
					else if ((mask & Queens) > 0) { score += 10 * multiplier; }
				}
			}

			return score;
		}

		public BoardStatus ExecuteMove(int move)
        {
			return ExecuteMove(DetermineFullMove(move));
        }

		public Move DetermineFullMove(int move)
        {
			// Only contains the positional information, the rest must be calculated.
			var fullMove = new Move(move);
			var start = new Square(this, fullMove.StartFile, fullMove.StartRank);
			var end = new Square(this, fullMove.EndFile, fullMove.EndRank);
			var struck = end.Piece != PieceType.None;
			var letter = ' ';
			var status = BoardStatus.Safe;

			letter = Piece.GetLetter(start.Piece, WhiteToMove);

            return new Move(fullMove.StartFile, fullMove.StartRank, fullMove.EndFile, fullMove.EndRank, WhiteToMove, end.Piece != PieceType.None, status, letter);
        }

		public BoardStatus ExecuteMove(Move move)
		{
			var start = new Square(this, move.StartFile, move.StartRank);
			var end = new Square(this, move.EndFile, move.EndRank);

			// Get rid of the piece in the original position and add it in the target position.
			ulong change = (~start.Mask) | end.Mask;
			Pieces &= change;
			switch(start.Piece)
            {
				case PieceType.None:
				case PieceType.Pawn:
					// TODO: check for en-passant to also remove the pawn from the actual origninal position.
					break;
				case PieceType.Knight: Knights &= change; break;
				case PieceType.Bishop: Bishops &= change; break;
				case PieceType.Rook: Rooks &= change; break;
				case PieceType.Queen: Queens &= change; break;
				case PieceType.King: Kings &= change; break;
            }

			if (start.Piece != end.Piece)
			{
				switch (end.Piece)
				{
					case PieceType.None: break;
					case PieceType.Pawn: Pawns &= (~end.Mask); break;
					case PieceType.Knight: Knights &= (~end.Mask); break;
					case PieceType.Bishop: Bishops &= (~end.Mask); break;
					case PieceType.Rook: Rooks &= (~end.Mask); break;
					case PieceType.Queen: Queens &= (~end.Mask); break;
					case PieceType.King: Kings &= (~end.Mask); break;
				}
			}

			return BoardStatus.Safe;
		}
	}
}
