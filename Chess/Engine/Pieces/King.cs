using System;
using System.Collections.Generic;

namespace Engine.Pieces
{
	public class King : Piece
	{
		public King(bool white, Field field) : base(white, field)
		{
		}

		public override int Points()
		{
			return 0;
		}

		public override char Letter()
		{
			return this.White ? 'K' : 'k';
		}

		public override void CalculateMoves(List<Move> moves)
		{
			foreach (var offset in Piece.AllSurroundingOffsets)
			{
				var rank = this.Field.Rank + offset.Rank;
				var file = this.Field.File + offset.File;

				if (rank >= 0 && rank < Board.Size && file >= 0 && file < Board.Size)
				{
					var target = Board.Active.Fields[file, rank];
					if (Piece.CanFieldBeAttacked(target, this) == false)
						this.MakeMove(target, moves);
				}
			}
		}

		public IEnumerable<AttackCombo> AnalyzeThreats(Board board)
		{
			var direction = board.WhiteToMove ? 1 : -1;

			// Check if pawns can strike the field.
			byte targetRank = (byte)(this.Field.Rank + direction);
			var left = this.Field.File - 1;

			if (left >= 0)
			{
				var piece = Board.Active.Fields[left, targetRank].Piece;
				if (piece != null && piece.White != board.WhiteToMove && piece is Pawn)
					yield return new AttackCombo
					{
						Attacker = piece,
						King = this,
					};
			}

			var right = this.Field.File + 1;
			if (right < Board.Size)
			{
				var piece = Board.Active.Fields[right, targetRank].Piece;
				if (piece != null && piece.White != board.WhiteToMove && piece is Pawn)
					yield return new AttackCombo
					{
						Attacker = piece,
						King = this,
					};
			}

			// Check if Knights can strike the field.
			foreach (var jump in KnightJumps)
			{
				var rank = this.Field.Rank + jump.Rank;
				var file = this.Field.File + jump.File;

				if (rank >= 0 && rank < Board.Size && file >= 0 && file < Board.Size)
				{
					var piece = Board.Active.Fields[file, rank].Piece;
					if (piece != null && piece.White != board.WhiteToMove && piece is Knight)
						yield return new AttackCombo
						{
							Attacker = piece,
							King = this,
						};
				}
			}

			// Check for pieces that can move in a line
			foreach (var offset in AllSurroundingOffsets)
			{
				// Check if the difference between the two offset parts is 1, in which case the offset points to a straight line.
				var straight = ((offset.Rank + offset.File) & 1) == 1;
				for (byte file = (byte)(this.Field.File + offset.File), rank = (byte)(this.Field.Rank + offset.Rank); file >= 0 && file < Board.Size && rank >= 0 && rank < Board.Size; file = (byte)(file + offset.File), rank = (byte)(rank + offset.Rank))
				{
					Piece pinned = null;
					var piece = Board.Active.Fields[file, rank].Piece;
					if (piece != null)
						if (piece.White != board.WhiteToMove)
						{
							if ((straight ? piece is Rook : piece is Bishop ) || (piece is Queen))
								yield return new AttackCombo
								{
									Attacker = piece,
									Pinned = pinned,
									King = this,
								};

							// Never continue after an opposing piece has been encountered.
							break;
						}
						else
						{
							if (pinned == null)
								pinned = piece;
							// Break if a second piece of the same color has been found.
							else break;
						}
				}
			}
		}
	}
}