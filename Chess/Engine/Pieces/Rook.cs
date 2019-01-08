using System;
using System.Collections.Generic;

namespace Engine.Pieces
{
	public class Rook : Piece
	{
		public Rook(bool white, Field field) : base(white, field)
		{
		}

		public override int Points()
		{
			return 5;
		}

		public override char Letter()
		{
			return this.White ? 'R' : 'r';
		}

		public override void CalculateMoves(List<Move> moves)
		{
			this.GetMovesStraight(moves);
		}
	}
}