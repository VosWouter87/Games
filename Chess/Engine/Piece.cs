namespace Engine
{
    public enum PieceType
    {
		None, Pawn, Knight, Bishop, Rook, Queen, King
    };

    internal class Piece
	{
		public static char GetLetter(PieceType piece, bool white)
		{
			char letter = ' ';
			switch (piece)
			{
				case PieceType.Pawn:
					letter = 'p';
					break;
				case PieceType.Knight:
					letter = 'n';
					break;
				case PieceType.Bishop:
					letter = 'b';
					break;
				case PieceType.Rook:
					letter = 'r';
					break;
				case PieceType.Queen:
					letter = 'q';
					break;
				case PieceType.King:
					letter = 'k';
					break;
			}

			if (white)
			{
				letter = char.ToUpper(letter);
			}

			return letter;
		}

		internal static void AddPawnMovesUnpinned(int square, Board board, List<int> moves)
        {
			if (board.WhiteToMove)
			{
				var step = square + Board.Dimension;
				if (step < Board.Size)
				{
					var file = step % Board.Dimension;
					if (file > 0)
					{
						var right = step - 1;
						if ((BitSupport.Basic[right] & board.Capturable) > 0)
						{
							moves.Add(board.CreateMoveSimple(square, right));
						}
					}
					//if (file < (Dimension - 1))
					if (file < 7)
					{
						var left = step + 1;
						if ((BitSupport.Basic[left] & board.Capturable) > 0)
						{
							moves.Add(board.CreateMoveSimple(square, left));
						}
					}
					if ((BitSupport.Basic[step] & board.Empty) > 0)
					{
						moves.Add(board.CreateMoveSimple(square, step));
						if ((square / Board.Dimension) == 1)
						{
							var two = step + Board.Dimension;
							if ((BitSupport.Basic[two] & board.Empty) > 0)
							{
								moves.Add(board.CreateMoveSimple(square, two));
							}
						}
					}
				}
			}
			else
			{
				var step = square - Board.Dimension;
				if (step >= 0)
				{
					var file = step % Board.Dimension;
					if (file > 0)
					{
						var right = step - 1;
						if ((BitSupport.Basic[right] & board.Capturable) > 0)
						{
							moves.Add(board.CreateMoveSimple(square, right));
						}
					}
					if (file > 0)
					{
						var left = step + 1;
						if ((BitSupport.Basic[left] & board.Capturable) > 0)
						{
							moves.Add(board.CreateMoveSimple(square, left));
						}
					}
					if ((BitSupport.Basic[step] & board.Empty) > 0)
					{
						moves.Add(board.CreateMoveSimple(square, step));
						if ((square / 8) == 7)
						{
							var two = step + Board.Dimension;
							if ((BitSupport.Basic[two] & board.Empty) > 0)
							{
								moves.Add(board.CreateMoveSimple(square, two));
							}
						}
					}
				}
			}
		}

		internal static void AddKnightMovesUnpinned(int square, Board board, List<int> moves)
        {
			ulong options = BitSupport.KnightJumps[square] & board.PossibleTargets;
			if (options > 0)
            {
				foreach(var jump in Board.KnightJumps)
                {
					var move = square + jump;
					if (move >= 0 && move < Board.Size && ((1UL << move) & options) > 0)
                    {
						moves.Add(board.CreateMoveSimple(square, move));
                    }
                }
			}
		}

		internal static void AddDiagonalMovesUnpinned(int square, Board board, List<int> moves)
		{
			ulong options = BitSupport.Diagonals[square] & board.PossibleTargets;
			if (options > 0)
            {
				foreach(var offset in Board.DiagonalOffsets)
                {
					var move = square + offset;
					while (move >= 0 && move < Board.Size && ((1UL << move) & options) > 0)
					{
						moves.Add(board.CreateMoveSimple(square, move));

						if (((1UL << move) & board.Opponent.Pieces) > 0)
                        {
							break;
                        }
					}
                }
            }
		}

		internal static void AddStraightMovesUnpinned(int square, Board board, List<int> moves)
		{
			ulong options = BitSupport.Straights[square] & board.PossibleTargets;
			if (options > 0)
			{
				foreach (var offset in Board.StraightOffsets)
				{
					var move = square + offset;
					while (move >= 0 && move < Board.Size && ((1UL << move) & options) > 0)
					{
						moves.Add(board.CreateMoveSimple(square, move));

						if ((BitSupport.Basic[move] & board.Opponent.Pieces) > 0)
						{
							break;
						}
					}
				}
			}
		}
		
		internal static void AddBlockLineMoves(int target, Board board, List<int> moves)
		{
			var targetFile = (byte)(target & 7);
			var targetRank = (byte)((target & 56) >> 3);
			var direction = board.WhiteToMove ? -1 : 1;
			var sourceRank = targetRank + direction;
			var pawnMasks = new ulong[0];
			var pawnOffsets = new int[0];

			if (board.WhiteToMove)
            {
				pawnMasks = BitSupport.WhitePawns;
				pawnOffsets = Board.WhitePawnOffsets;
            }
			else
            {
				pawnMasks = BitSupport.BlackPawns;
				pawnOffsets = Board.BlackPawnOffsets;
            }

			if (((1UL << target) & board.Empty) > 0)
			{
				foreach (var off in pawnOffsets)
				{
					var source = target + off;
					if (Math.Abs(off) % 2 == 0)
					{
						if (source >= 0 && source < Board.Size && (BitSupport.Basic[source] & board.Pawns & board.Active.Pieces) > 0)
						{
							moves.Add(board.CreateMoveSimple(source, target));
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
			if (((1UL << target) & board.PossibleTargets) > 0)
			{
				foreach (var jump in Board.KnightJumps)
				{
					var source = target + jump;
					if (source >= 0 && source < Board.Size && ((1UL << source) & BitSupport.KnightJumps[source]) > 0)
					{
						moves.Add(board.CreateMoveSimple(source, target));
					}
				}

				for (var i = 0; i < Board.AllDirections.Length; i++)
                {
					var offset = Board.AllDirections[i];
					var source = target + offset;
					ulong[] line = BitSupport.Lines[i];
					while (source >= 0 && source < Board.Size && ((1UL << source) & board.PossibleTargets & line[source]) > 0)
                    {
						moves.Add(board.CreateMoveSimple(source, target));

						// If a piece was taken stop using the offset
						if ((BitSupport.Basic[source] & board.Opponent.Pieces & line[source]) > 0)
                        {
							continue;
                        }
						source += offset;
                    }
                }
			}
		}

		/// <summary>
		/// So not moves defending the king against an attacker.
		/// </summary>
		internal static void AddSafeKingMoves(int king, Board board, List<int> moves)
		{
			// TODO: In order to find safe fields pieces other than kings must also be considered.
			var options = BitSupport.KingMoveOptions[king] & board.Opponent.Pieces | board.Empty
				& (~BitSupport.KingMoveOptions[board.WhiteToMove ? board.WhiteKing : board.BlackKing]); // Consider opponent king
			if (options > 0)
			{
				var rank = (byte)(king & 7);
				var file = (byte)((king & 56) >> 3);
				foreach (var o in Offset.AllDirections)
				{
					var row = rank + o.Rank;
					var col = file + o.File;
					if (row >= 0 && col >= 0 && row < Board.Dimension && col < Board.Dimension)
					{
						var square = row * Board.Dimension + col;
						if ((BitSupport.Basic[square] & options) > 0)
                        {
							moves.Add(board.CreateMoveSimple(row, col));
                        }
					}
				}
			}
		}
	}
}
