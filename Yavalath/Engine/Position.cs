namespace Engine
{
	public class Position
	{
		/// <summary>
		/// Each bit indicates whether a field has been occupied by a player.
		/// </summary>
		private readonly long taken;

		/// <summary>
		/// The first bit indicates which player's turn it is, each following bit indicates which color occupies a field.
		/// </summary>
		private readonly long occupiers;

		public Position(Board board)
		{
			taken = 0;
			occupiers = 0;
			var fieldMask = 1;
			var takenMask = 2; // The first index indicates who's turn it is.
			
			if (board.WhiteToMove)
			{
				occupiers &= 1;
			}

			foreach (var field in board.AllFields())
			{
				if (field.Occupant != null)
				{
					taken &= fieldMask;

					if (field.Occupant.IsWhite)
					{
						occupiers &= takenMask;
						takenMask >>= 1;
					}

					fieldMask >>= 1;
				}
			}
		}

		public Board ToBoard(Player white, Player black)
		{
			var board = new Board();
			var fieldMask = 1;
			var takenMask = 2; // The first index indicates who's turn it is.
			board.WhiteToMove = (occupiers & 1) == 1;

			foreach (var field in board.AllFields())
			{
				if ((taken & fieldMask) == 1)
				{
					if ((occupiers & takenMask) == 1)
					{
						field.TakeField(white);
					}
					else
					{
						field.TakeField(black);
					}
				}

				fieldMask >>= 1;
			}

			return board;
		}
	}
}
