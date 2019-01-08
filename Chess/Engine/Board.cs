using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.Pieces;

namespace Engine
{
	public class Board
	{
		public static string Default = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

		private static Board _active;
		public static Board Active { get { return _active; } }

		/// <summary>
		/// The algorithm works with bytes, so it will fail to detect many cases if this number is 256.
		/// </summary>
		public const int Size = 8;
		public CastlingOptions CastlingMoved { get; set; } = 0;
		public KingStatus KingStatus { get; set; } = KingStatus.Unknown;
		public byte[] KingLocations = new byte[4];
		public Field EnPassantTargetSquare { get; set; } = null;
		public bool WhiteToMove { get; set; } = true;
		public Field[,] Fields { get; set; } = new Field[Size, Size];
		public King WhiteKing { get; set; }
		public King BlackKing { get; set; }
		public List<AttackCombo> WhiteCombos = new List<AttackCombo>();
		public List<AttackCombo> BlackCombos = new List<AttackCombo>();
		public int HalfMoveClock = 0;
		public int FullMoves = 1;
		
		/// <summary>
		/// The default constructor will result in an empty board.
		/// </summary>
		public Board()
		{
			_active = this;
		}

		public Board(string ForsythEdwardsNotation)
		{
			_active = this;
			var parts = ForsythEdwardsNotation.Split(' ');

			byte rank = (byte)Board.Size;
			foreach (var line in parts[0].Split('/'))
			{
				byte file = 0;
				rank--;
				foreach (var letter in line)
				{
					if (char.IsDigit(letter))
					{
						byte count = (byte)(letter - Constants.RankNumberOffset);
						for (var i = 0; i < count; i++)
						{
							this.Fields[file, rank] = new Field(file, rank);
							file++;
						}
					}
					else
					{
						var field = new Field(file, rank);
						this.Fields[file, rank] = field;
						file++;

						switch (letter)
						{
							case 'P': field.Piece = new Pawn(true, field); break;
							case 'B': field.Piece = new Bishop(true, field); break;
							case 'N': field.Piece = new Knight(true, field); break;
							case 'R': field.Piece = new Rook(true, field); break;
							case 'K':
								this.WhiteKing = new King(true, field);
								field.Piece = this.WhiteKing;
								break;
							case 'Q': field.Piece = new Queen(true, field); break;
							case 'p': field.Piece = new Pawn(false, field); break;
							case 'b': field.Piece = new Bishop(false, field); break;
							case 'n': field.Piece = new Knight(false, field); break;
							case 'r': field.Piece = new Rook(false, field); break;
							case 'k':
								this.BlackKing = new King(false, field);
								field.Piece = this.BlackKing;
								break;
							case 'q': field.Piece = new Queen(false, field); break;
							default: throw new Exception("Input is not a valid chess piece: " + letter);
						}
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
				this.EnPassantTargetSquare = this.Fields[enPassantTargetSquare[0] - Constants.FileLetterOffset, enPassantTargetSquare[1] - Constants.RankNumberOffset];
			}

			var halfMoveClock = parts[4];
			if (!int.TryParse(halfMoveClock, out this.HalfMoveClock))
				throw new Exception("Half move clock number isn't proper FEN notation: https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation " + halfMoveClock);

			var fullMoves = parts[5];
			if (!int.TryParse(fullMoves, out this.FullMoves))
				throw new Exception("Full move number isn't proper FEN notation: https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation " + fullMoves);
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			for (var file = 0; file < Size; file++)
			{
				for (var rank = 0; rank < Size; rank++)
					if (this.Fields[file, rank].Piece == null)
						sb.Append(' ');
					else sb.Append(this.Fields[file, rank].Piece.Letter());
				sb.AppendLine();
			}

			return sb.ToString();
		}

		public int AnalyzeKingStatus()
		{
			if (this.WhiteToMove)
			{
				WhiteCombos = new List<AttackCombo>(WhiteKing.AnalyzeStatus());
				return WhiteCombos.Count;
			}
			else
			{
				BlackCombos = new List<AttackCombo>(BlackKing.AnalyzeStatus());
				return BlackCombos.Count;
			}
		}

		public AttackCombo GetPinned(Piece piece)
		{
			foreach (var combo in WhiteCombos)
				if (combo.Pinned != null && combo.Pinned == piece)
					return combo;

			return null;
		}

		public List<Move> CalculateMoves()
		{
			var moves = new List<Move>();

			if (KingStatus == KingStatus.Unknown)
			{
				var attackerCount = this.AnalyzeKingStatus();
				KingStatus = attackerCount == 0 ? KingStatus.Safe : KingStatus.Checked;
			}

			if (KingStatus == KingStatus.Safe)
			{
				for (var file = 0; file < Size; file++)
					for (var rank = 0; rank < Size; rank++)
					{
						var piece = this.Fields[file, rank].Piece;
						if (piece != null && piece.White == this.WhiteToMove)
							piece.CalculateMoves(moves);
					}
			}
			else if (KingStatus == KingStatus.Checked)
			{
				// Always check if the king can move away.
				(this.WhiteToMove ? this.WhiteKing : this.BlackKing).CalculateMoves(moves);

				var attackers = this.WhiteCombos.Where(ac => ac.King.White == this.WhiteToMove && ac.Pinned == null).ToArray();
				if (attackers.Length == 1)
				{
					var combo = attackers[0];
					if ((combo.Attacker is Pawn || combo.Attacker is Knight) == false)
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
				KingStatus = KingStatus == KingStatus.Checked ? KingStatus.Mated : KingStatus.Draw;
			}

			return moves;
		}

		public string Reverse(string setup)
		{
			var result = new StringBuilder(setup);
			var half = setup.Length >> 1;
			for (var i = 0; i < half; i++)
			{
				result[i] = setup[setup.Length - i - 1];
				result[setup.Length - i - 1] = setup[i];
			}

			return result.ToString();
		}

		public double CalculateScore()
		{
			var score = 0.0;
			for (var file = 0; file < Size; file++)
				for (var rank = 0; rank < Size; rank++)
				{
					var piece = this.Fields[file, rank].Piece;
					if (piece != null && piece.White == this.WhiteToMove)
						score += piece.Points();
				}

			return score;
		}
	}
}
