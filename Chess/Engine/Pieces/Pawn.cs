using System.Collections.Generic;

namespace Engine.Pieces
{
	public class Pawn : Piece
	{
		public Pawn(bool white, Field field) : base(white, field)
		{
		}

		public override int Points()
		{
			return 1;
		}

		public override char Letter()
		{
			return this.White ? 'P' : 'p';
		}

		private int Direction()
		{
			return this.White ? 1 : -1;
		}

		private byte StartRow()
		{
			return (byte)(this.White ? 1 : 6);
		}

		public override void CalculateMoves(List<Move> moves)
		{
			// If there's an attacker check if the pawn can move in that direction or strike the attacker.
			var combo = Board.Active.GetPinned(this);
			var direction = this.Direction();
			byte targetRank = (byte)(this.Field.Rank + direction);
			
			if (combo == null || this.Field.File == combo.Attacker.Field.File)
			{
				if (Board.Active.Fields[this.Field.File, targetRank].Piece == null)
				{
					moves.Add(new Move(this.Field.File, this.Field.Rank, this.Field.File, targetRank, this.White, false, KingStatus.Unknown, this.Letter()));
					if (this.Field.Rank == this.StartRow() && Board.Active.Fields[this.Field.File, targetRank + direction].Piece == null)
						moves.Add(new Move(this.Field.File, this.Field.Rank, this.Field.File, (byte)(targetRank + direction), this.White, false, KingStatus.Unknown, this.Letter()));
				}
			}
			
			var left = this.Field.File - 1;
			if (left >= 0 && (combo == null || (combo.Attacker.Field.File == left && combo.Attacker.Field.Rank == targetRank)))
			{
				var target = Board.Active.Fields[left, this.Field.Rank + direction];
				if (target.Piece != null && target.Piece.White != this.White)
					moves.Add(new Move(this.Field.File, this.Field.Rank, (byte)left, targetRank, this.White, true, KingStatus.Unknown, this.Letter()));
			}

			var right = this.Field.File + 1;
			if (right < Board.Size && (combo == null || (combo.Attacker.Field.File == right && combo.Attacker.Field.Rank == targetRank)))
			{
				var target = Board.Active.Fields[right, this.Field.Rank + direction];
				if (target.Piece != null && target.Piece.White != this.White)
					moves.Add(new Move(this.Field.File, this.Field.Rank, (byte)right, targetRank, this.White, true, KingStatus.Unknown, this.Letter()));
			}
		}
	}
}