using System;

namespace CantStop
{
	public class Marker
	{
		public Column Column { get; private set; }
		public int Position { get; private set; }

		public void SetMarker(Column column, int start)
		{
			if (Column == null)
			{
				Column = column;
				Position = start;
			}
			else throw new Exception("Clear the marker first.");
		}

		public int Stop()
		{
			var result = Position;
			this.Abort();
			return result;
		}

		public void Abort()
		{
			Column = null;
			Position = 0;
		}
	}
}
