using System;
using System.Collections.Generic;

namespace Engine.Pieces
{
	public class Bishop : Piece
	{
		public Bishop(bool white, Field field) : base(white, field)
		{
		}

		public override int Points()
		{
			return 3;
		}

		public override char Letter()
		{
			return this.White ? 'B' : 'b';
		}

		public override void CalculateMoves(List<Move> moves)
		{
			this.GetMovesDiagonally(moves);
		}
	}
}
