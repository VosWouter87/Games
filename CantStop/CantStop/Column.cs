namespace CantStop
{
	public class Column
	{
		public int Target { get; }
		public int Length { get; }
		public bool Available { get; set; } = true;
		public int[] Squares { get; }

		public Column(int target, int length, int playerCount)
		{
			Target = target;
			Length = length;
			Squares = new int[playerCount];
		}
	}
}
