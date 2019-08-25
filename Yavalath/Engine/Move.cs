namespace Engine
{
	public struct Move
	{
		public readonly byte Col;
		public readonly byte Row;
		public readonly bool White;

		public Move(byte col, byte row, bool white)
		{
			Col = col;
			Row = row;
			White = white;
		}

		public string ToString()
		{
			return $"{ 'a' + Col }{ '0' + Row } - { (White ? 'w' : 'b') }";
		}
	}

	public enum Result
	{
		Play,
		Rejected,
		Loss,
		Win,
	}
}
