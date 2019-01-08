namespace Engine
{
	public struct Offset
	{
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
