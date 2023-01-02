namespace Engine
{
	public struct Offset
	{
		public static Offset NorthWest = new Offset(-1, 1);
		public static Offset North = new Offset(0, 1);
		public static Offset NorthEast = new Offset(1, 1);
		public static Offset East = new Offset(1, 0);
		public static Offset SouthEast = new Offset(1, -1);
		public static Offset South = new Offset(0, -1);
		public static Offset SouthWest = new Offset(-1, -1);
		public static Offset West = new Offset(-1, 0);
		public static Offset[] Straights = new Offset[] { North, East, South, West };
		public static Offset[] Diagonals = new Offset[] { NorthWest, NorthEast, SouthEast, SouthWest };
		public static Offset[] AllDirections = new Offset[] { NorthWest, North, NorthEast, East, SouthEast, South, SouthWest, West };
		public static Offset[] Jumps = new Offset[] { new Offset(-2, -1), new Offset(-1, -2), new Offset(1, -2), new Offset(2, -1), new Offset(2, 1), new Offset(1, 2), new Offset(-1, 2), new Offset(-2, 1) };

		public readonly int Rank;
		public readonly int File;

		public Offset(int rank, int file)
		{
			this.Rank = rank;
			this.File = file;
		}

		public override string ToString()
		{
			return "" + this.File + "," + this.Rank;
		}
	}
}
