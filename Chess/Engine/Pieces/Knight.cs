using System;
using System.Collections.Generic;

namespace Engine.Pieces
{
	public class Knight : Piece
	{
		public Knight(bool white, Field field) : base(white, field)
		{
		}

		public override int Points()
		{
			return 3;
		}

		public override char Letter()
		{
			return this.White ? 'N' : 'n';
		}

		public override void CalculateMoves(List<Move> moves)
		{
			if (Board.Active.GetPinned(this) == null)
				foreach(var jump in KnightJumps)
				{
					var file = this.Field.File + jump.File;
					var rank = this.Field.Rank + jump.Rank;

					if (file >= 0 && file < Board.Size && rank >= 0 && rank < Board.Size)
						this.MakeMove(Board.Active.Fields[file, rank], moves);
				}
		}
	}
}