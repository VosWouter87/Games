using System;
using System.Collections.Generic;

namespace Engine.Pieces
{
	public abstract class Piece
	{
		public readonly bool White;
		public Field Field { get; set; }

		public abstract int Points();
		public abstract char Letter();
		public abstract void CalculateMoves(List<Move> moves);
		public static Offset[] AdjacentOffsets = new Offset[] { new Offset(1, 0), new Offset(0, -1), new Offset(-1, 0), new Offset(0, 1) };
		public static Offset[] CornerOffsets = new Offset[] { new Offset(1, 1), new Offset(1, -1), new Offset(-1, -1), new Offset(-1, 1) };
		public static Offset[] AllSurroundingOffsets = new Offset[] { new Offset(1, 1), new Offset(1, 0), new Offset(1, -1), new Offset(0, -1), new Offset(-1, -1), new Offset(-1, 0), new Offset(-1, 1), new Offset(0, 1) };
		public static Offset[] KnightJumps = new Offset[] { new Offset(2, 1), new Offset(1, 2), new Offset(-1, 2), new Offset(-2, 1), new Offset(-1, -2), new Offset(-2, -1), new Offset(1, -2), new Offset(2, -1) };

		public Piece(bool white, Field field)
		{
			this.White = white;
			this.Field = field;
		}

		public override string ToString()
		{
			return this.Field.ToString();
		}

		public void MakeMove(Field target, List<Move> moves)
		{
			if (target.Piece == null || this.White != target.Piece.White)
				// You cannot check yourself, so a king move ais always safe, otherwise we're not going to calculate.
				moves.Add(new Move(this.Field.File, this.Field.Rank, target.File, target.Rank, this.White, target.Piece != null && this.White != target.Piece.White, this is King ? KingStatus.Safe : KingStatus.Unknown, this.Letter()));
		}

		public void GetMovesDiagonally(List<Move> moves)
		{
			foreach (var direction in CornerOffsets)
				for (int file = Field.File + direction.File, rank = Field.Rank + direction.Rank; file >= 0 && file < Board.Size && rank >= 0 && rank < Board.Size; file += direction.File, rank += direction.Rank)
				{
					var piece = Board.Active.Fields[file, rank].Piece;
					if (piece == null)
						this.MakeMove(Board.Active.Fields[file, rank], moves);
					else
					{
						if (this.White != piece.White)
							this.MakeMove(Board.Active.Fields[file, rank], moves);

						// We cannot move further if there's another piece.
						break;
					}
				}
		}

		public void GetMovesStraight(List<Move> moves)
		{
			foreach (var offset in AdjacentOffsets)
				for (byte file = (byte)(Field.File + offset.File), rank = (byte)(Field.Rank + offset.Rank); file >= 0 && file < Board.Size && rank >= 0 && rank < Board.Size; file = (byte)(file + offset.File), rank = (byte)(rank + offset.Rank))
				{
					var piece = Board.Active.Fields[file, rank].Piece;
					if (piece == null)
						this.MakeMove(Board.Active.Fields[file, rank], moves);
					else
					{
						if (this.White != piece.White)
							this.MakeMove(Board.Active.Fields[file, rank], moves);

						// We cannot move further if there's another piece.
						break;
					}
				}
		}
		
		public static void AddBlockLineMoves(Field target, List<Move> moves)
		{
			var white = Board.Active.WhiteToMove;
			var direction = white ? -1 : 1;
			var sourceRank = target.Rank + direction;

			// A pawn could never exist in such a field, but the method may ask for checking such a field.
			if (sourceRank >= 0 && sourceRank < Board.Size)
			{
				var piece = Board.Active.Fields[sourceRank, target.File].Piece;
				if (piece != null && piece.White == white && piece is Pawn)
					moves.Add(new Move(piece.Field.Rank, piece.Field.File, target.Rank, target.File, white, false, KingStatus.Unknown, piece.Letter()));
			}

			sourceRank = sourceRank + direction;
			if (sourceRank == (white ? 1 : 6))
			{
				var source = Board.Active.Fields[sourceRank, target.File];
				if (source.Piece != null && source.Piece.White == white && source.Piece is Pawn)
					moves.Add(new Move(source.Rank, source.File, target.Rank, target.File, white, false, KingStatus.Unknown, source.Piece.Letter()));
			}

			foreach (var jump in KnightJumps)
			{
				var rank = target.Rank + jump.Rank;
				var file = target.File + jump.File;

				if (rank >= 0 && rank < Board.Size && file >= 0 && file < Board.Size)
				{
					var piece = Board.Active.Fields[file, rank].Piece;
					if (piece != null && piece.White == white && piece is Knight)
						moves.Add(new Move((byte)rank, (byte)file, target.Rank, target.File, white, false, KingStatus.Unknown, piece.Letter()));
				}
			}
			
			// Check for pieces that can move in a line
			foreach (var offset in AllSurroundingOffsets)
			{
				// Check if the difference between the two offset parts is 1, in which case the offset points to a straight line.
				var straight = ((offset.Rank + offset.File) & 1) == 1;
				for (byte file = (byte)(target.File + offset.File), rank = (byte)(target.Rank + offset.Rank); file >= 0 && file < Board.Size && rank >= 0 && rank < Board.Size; file = (byte)(file + offset.File), rank = (byte)(rank + offset.Rank))
				{
					var piece = Board.Active.Fields[file, rank].Piece;
					if (piece != null)
					{
						if (piece.White == white && ((straight ? piece is Rook : piece is Bishop) || piece is Queen))
							moves.Add(new Move(file, rank, target.Rank, target.File, white, false, KingStatus.Unknown, piece.Letter()));

						// Never continue after a piece has been encountered.
						break;
					}
				}
			}
		}

		public static bool CanFieldBeAttacked(Field target, King king)
		{
			var white = Board.Active.WhiteToMove == false;
			var direction = white ? 1 : -1;

			// Check if pawns can strike the field.
			byte targetRank = (byte)(target.Rank + direction);
			var left = target.File - 1;

			if (left >= 0 && targetRank > 0 && targetRank < Board.Size)
			{
				var piece = Board.Active.Fields[left, targetRank].Piece;
				if (piece != null && piece.White == white && piece is Pawn)
					return true;
			}

			targetRank = (byte)(targetRank + direction);
			var right = target.File + 1;
			if (right < Board.Size && targetRank > 0 && targetRank < Board.Size)
			{
				var piece = Board.Active.Fields[right, targetRank].Piece;
				if (piece != null && piece.White == white && piece is Pawn)
					return true;
			}

			// Check if Knights can strike the field.
			foreach (var jump in KnightJumps)
			{
				var file = target.File + jump.File;
				var rank = target.Rank + jump.Rank;

				if (file >= 0 && file < Board.Size && rank >= 0 && rank < Board.Size)
				{
					var piece = Board.Active.Fields[file, rank].Piece;
					if (piece != null && piece.White == white && piece is Knight)
						return true;
				}
			}

			// Check for pieces that can move in a line
			foreach (var offset in AllSurroundingOffsets)
			{
				// Check if the difference between the two offset parts is 1, in which case the offset points to a straight line.
				var straight = ((offset.Rank + offset.File) & 1) == 1;
				for (byte file = (byte)(target.File + offset.File), rank = (byte)(target.Rank + offset.Rank); file >= 0 && file < Board.Size && rank >= 0 && rank < Board.Size; file = (byte)(file + offset.File), rank = (byte)(rank + offset.Rank))
				{
					var first = true;
					var piece = Board.Active.Fields[file, rank].Piece;
					if (piece != null && piece != king)
					{
						if (piece.White == white && ((straight ? piece is Rook : piece is Bishop) || piece is Queen || (first && piece is King)))
							return true;

						// Never continue after a piece has been encountered.
						break;
					}
				}
			}

			return false;
		}
	}
}
