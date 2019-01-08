using System;
using Engine.Pieces;

namespace Engine
{
	/// <summary>
	/// The following information needs to be stored in this class and how many bits that may cost:
	/// 4 Whether either of the kings can castle in both directions.
	/// 7 If a pawn has just done a double step forward, contains the field behind it.
	/// 1 For which players turn it is, 1 for white 0 for black.
	/// 32 for fields without a piece with a simple 0 for no piece.
	/// 32 for the color of the piece on fields that have a piece, 1 for white 0 for black
	/// 16 for pawns with a 1 for a pawn and 0 for other piece. So 101 for a black pawn.
	/// 24 for the regular pieces that each player has 2 of. Total for white pieces: Bishops (11011), Knights (11010) and Rooks (11001).
	/// 4 for the King and Queen on each side. Their digits start with 00 instead of the bits for the previous and a 1 added for King and 0 added for Queen.
	/// For a maximum of 120 bits.
	/// </summary>
	public struct Position
	{
		private long _first, _second;
		
		public Position(Board board)
		{
			this._first = (int)board.CastlingMoved;
			this._second = 0;
			if (board.EnPassantTargetSquare != null)
			{
				this._first += 64; // Bit 7.
				this._first += board.EnPassantTargetSquare.File << 8;
				this._first += board.EnPassantTargetSquare.Rank << 11;
			}

			if (board.WhiteToMove)
				this._first += 8192;

			var currentBit = 15;

			for (var rank = 0; rank < Board.Size; rank++)
				for (var file = 0; file < Board.Size; file++)
				{
					var piece = board.Fields[rank, file].Piece;
					WriteBit(currentBit++, piece != null);

					if (piece != null)
					{
						WriteBit(currentBit++, piece.White);

						var isPawn = piece is Pawn;
						WriteBit(currentBit++, isPawn);

						if (isPawn == false)
						{
							if (piece is Bishop)
							{
								WriteBit(currentBit++, true);
								WriteBit(currentBit++, true);
							}
							else if (piece is Knight)
							{
								WriteBit(currentBit++, true);
								currentBit++;
							}
							else if (piece is Rook)
							{
								currentBit++;
								WriteBit(currentBit++, true);
							}
							else
							{
								currentBit++;

								if (piece is King)
									WriteBit(currentBit++, true);
								else if (piece is Queen)
									currentBit++;
								else throw new Exception("Object is of unknown class: " + piece.GetType());
							}
						}
					}
				}
		}

		public Board ToBoard()
		{
			var board = new Board();
			board.CastlingMoved = (CastlingOptions)(this._first & 63); // And with the first 6 bits being 1.
			if ((this._first & 64) == 1)
			{
				var pawnFile = (byte)(this._first & 896); // Three 1 bits after 7 bits 0.
				var pawnRank = (byte)(this._first & 7168);
				board.EnPassantTargetSquare = board.Fields[pawnFile, pawnRank];
			}

			board.WhiteToMove = (this._first & 8192) == 1;

			var currentBit = 12;
			int rank = 0, file = 0;
			
			while(file < Board.Size)
			{
				var field = new Field((byte)rank, (byte)file);
				if (ReadBit(currentBit++))
				{
					// There's a piece on the field.
					var white = ReadBit(currentBit++);
					if (ReadBit(currentBit++))
						field.Piece = new Pawn(white, field);
					else if (ReadBit(currentBit++))
						// The piece may be a Bishop or Knight.
						if (ReadBit(currentBit++))
							field.Piece = new Bishop(white, field);
						else field.Piece = new Knight(white, field);
					else if (ReadBit(currentBit++))
						field.Piece = new Rook(white, field);
					else if (ReadBit(currentBit++))
					{
						var king = new King(white, field);
						field.Piece = king;

						if (white)
							board.WhiteKing = king;
						else board.BlackKing = king;
					}
					else field.Piece = new Queen(white, field);

					board.Fields[rank, file] = field;
				}

				rank++;
				if (rank == Board.Size)
				{
					rank = 0;
					file++;
				}
			}

			return board;
		}

		public bool ReadBit(int index)
		{
			if (index < 64)
				return (this._first & (1 << index)) == 1;
			else return (this._second & (1 << (index - 64))) == 1;
		}

		public void WriteBit(int index, bool value)
		{
			if (value)
			{
				if (index < 64)
					this._first &= (1 >> index);
				else
					this._second &= (1 >> (index - 64));
			}
		}
	}
}
