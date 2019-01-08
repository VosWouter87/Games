using System.Collections.Generic;

namespace Engine.Pieces
{
	public class Queen : Piece
	{
		public Queen(bool white, Field field) : base(white, field)
		{
		}

		public override int Points()
		{
			return 10;
		}

		public override char Letter()
		{
			return this.White ? 'Q' : 'q';
		}

		public override void CalculateMoves(List<Move> moves)
		{
			this.GetMovesStraight(moves);
			this.GetMovesDiagonally(moves);
		}
	}
}