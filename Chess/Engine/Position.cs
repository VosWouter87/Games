using System;

namespace Engine
{
	/// <summary>
	/// The following information needs to be stored in this class and how many bits that may cost:
	/// 64 for squares all squares, 0 for no piece and 1 to indicate a piece.
	/// 4 Whether either of the kings can castle in either direction.
	/// 1 If a pawn has just done a double step forward.
	/// 3 If it did to identify the column
	/// 1 For which player's turn it is, 1 for white 0 for black.
	/// 32 for the color of the piece on squares that have a piece, 1 for white 0 for black
	/// 32 So all pieces can be identified as pawn or not, 0 for a pawn and 1 for another piece. So 101 for a black pawn.
	/// 24 2 per regular piece 12 in total, that each player has 2 of. Total for white pieces: Bishops (11011), Knights (11010) and Rooks (11001).
	/// 4 for the King and Queen on each side. Their digits start with 00 instead of the bits for the previous and a 1 added for King and 0 added for Queen.
	/// For a maximum of 165 bits, 8 bits for the number of moves since the last strike or pawn moves aren't included.
	/// 64 + 4 + 5 + 32 + 32 + 24 + 4 = 165
	/// 4 + 5 + 32 + 32 + 24 + 4 = 101 (without indicators of piece existence)
	/// </summary>
	public struct Position : IComparable<Position>
	{
		private ulong pieces, whiteAndPawn, remainder;
		
		public Position(Board board)
		{
			pieces = board.Pieces;
			whiteAndPawn = 0;
			remainder = (uint)board.CastlingMoved;

			// Uint doesn't have negative or null, so use the size as a not-set value.
			if (board.DoubleMovedPawnFile <= Board.Size)
			{
				remainder += 16; // bit 5.
				remainder += (ulong)(board.DoubleMovedPawnFile << 5); // Three bits
			} // If not the bits just don't get written.

			if (board.WhiteToMove)
				remainder += 256; // bit 9 

			// These masks are for writing to the data
			ulong wpMask = 1;
			ulong otherMask = 512;

			// Mask is for reading from the board
			for (ulong mask = 1; mask < ulong.MaxValue; mask >>= 1)
            {
				if ((board.Pieces & mask) == mask)
				{
					if ((board.Whites & mask) == mask)
					{
						whiteAndPawn &= wpMask; // write 1
					}
					wpMask <<= 1; // Either write the bit to 1, or write it to 0 by skipping it.

					if ((board.Pawns & mask) == 0)
					{
						whiteAndPawn &= wpMask; // Write not a pawn 0
						if ((board.Knights & mask) == mask)
						{
							remainder &= otherMask; // Write a knight 10
							otherMask <<= 2;
						}
						else
						{
							if ((board.Bishops & mask) == mask)
							{
								remainder &= otherMask; // 1
								otherMask <<= 1;
								remainder &= otherMask; // 1
								otherMask <<= 1;
							}
							else
							{
								otherMask <<= 1; // 0
								if ((board.Rooks & mask) == mask)
								{
									remainder &= otherMask; // 1
									otherMask <<= 1;
								}
								else
								{
									otherMask <<= 1; // 0
									if ((board.Queens & mask) == mask)
									{
										otherMask <<= 1; // 0
									}
									else if ((board.Kings & mask) == mask)
									{
										remainder &= otherMask; // 1
										otherMask <<= 1;
									}
									else
                                    {
										throw new ArgumentException("Invalid input");
                                    }
								}
							}
						}
					}
					wpMask >>= 1; // The bit that indicates whether the piece is a pawn or not.
				}
			}
		}

		public Board ToBoard()
		{
			var board = new Board();
			board.Pieces = pieces;
			board.CastlingMoved = (CastlingOptions)(remainder & 15); // And with the first 6 bits being 1.
			if ((remainder & 16) == 1)
			{
				board.DoubleMovedPawnFile = (int)remainder & 224;
			}

			board.WhiteToMove = (remainder & 256) == 256;

			// These masks are for reading from the data
			ulong wpMask = 1;
			ulong otherMask = 512;

			// Mask is for writing to the board
			for (ulong mask = 1; mask < ulong.MaxValue; mask >>= 1)
			{
				if ((pieces & mask) == mask)
				{
					// This square does have a piece
					if ((whiteAndPawn & wpMask) == wpMask) // 1
					{
						board.Whites &= mask;
					}
					wpMask >>= 1; // 0 if it was black
					if ((whiteAndPawn & wpMask) == wpMask) // 1
					{
						board.Pawns &= mask;
					}
					else // 0
                    {
						// Check other piece types, wpMask will be moved later
						if ((remainder & otherMask) == otherMask) // 1
                        {
							otherMask >>= 1;
							if ((remainder & otherMask) == otherMask) // 1
							{
								board.Bishops &= mask;
							}
							else
							{
								board.Knights &= mask;
							}
							otherMask >>= 1;
						}
						else // 0
                        {
							otherMask >>= 1;
							if ((remainder & otherMask) == otherMask) // 1
                            {
								board.Rooks &= mask;
								otherMask >>= 1;
							}
							else // 0
                            {
								otherMask >>= 1;
								if ((remainder & otherMask) == otherMask) // 1
                                {
									board.Kings &= mask;
                                }
								else // 0
                                {
									board.Queens &= mask;
								}
								otherMask >>= 1; // Bit has been read
							}
						}
                    }

					wpMask <<= 1; // 0 if it wasn't a pawn
				}
			}

			return board;
		}
		/*
		private bool ReadBit(int index)
		{
			if (index < 64)
				return (this._whiteAndPawn & (1 << index)) == 1;
			else return (this._other & (1 << (index - 64))) == 1;
		}
		
		private void WriteBit(int index, bool value)
		{
			if (value)
			{
				if (index < 64)
					this._whiteAndPawn &= (1 >> index);
				else
					this._other &= (1 >> (index - 64));
			}
		}*/

		//
		// Summary:
		//     Compares the current instance with another object of the same type and returns
		//     an integer that indicates whether the current instance precedes, follows, or
		//     occurs in the same position in the sort order as the other object.
		//
		// Parameters:
		//   other:
		//     An object to compare with this instance.
		//
		// Returns:
		//     A value that indicates the relative order of the objects being compared. The
		//     return value has these meanings: Value Meaning Less than zero This instance precedes
		//     other in the sort order. Zero This instance occurs in the same position in the
		//     sort order as other. Greater than zero This instance follows other in the sort
		//     order.
		public int CompareTo(Position other)
        {
#if DEBUG
			if (pieces == other.pieces)
            {
				if (whiteAndPawn == other.whiteAndPawn)
                {
					if (remainder == other.remainder)
                    {
						if (remainder < other.remainder)
                        {
							return -1;
                        }
						else
                        {
							return 1;
                        }
                    } else
                    {
						if (whiteAndPawn < other.whiteAndPawn) 
						{
							return -1;
                        }
						else
                        {
							return 1;
                        }
                    }
                } else
                {
					if (remainder < other.remainder)
                    {
						return -1;
                    }
					else
                    {
						return 1;
                    }
                }
			}

			// Totally the same position
			return 0;
#else
			return pieces == other.pieces ?
				whiteAndPawn == other.whiteAndPawn ?
				remainder == other.remainder ?
				remainder < other.remainder ? -1 : 1 :
				whiteAndPawn < other.whiteAndPawn ? -1 : 1 :
				pieces < other.pieces ? -1 : 1
				: 0;
#endif
		}
	}
}
