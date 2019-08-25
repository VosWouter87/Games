namespace Engine
{
	public class Field
	{
		public readonly byte Col;
		public readonly byte Row;

		/// <summary>
		/// Which player has taken this field, null if the field is currently empty.
		/// </summary>
		public Player Occupant;

		public Field(byte col, byte row)
		{
			Col = col;
			Row = row;
		}

		public bool IsAvailable()
		{
			return Occupant == null;
		}

		public bool TakeField(Player occupier)
		{
			if (Occupant == null)
			{
				Occupant = occupier;
				return true;
			}

			return false;
		}
	}
}
