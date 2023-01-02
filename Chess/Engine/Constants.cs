using System;

namespace Engine
{
	public static class Constants
	{
		public const int FileLetterOffset = 97;
		public const int RankNumberOffset = 48;
	}

	[Flags]
	public enum CastlingOptions : short
	{
		WhiteKingSide = 1,
		WhiteQueenSide = 2,
		BlackKingSide = 4,
		BlackQueenSide = 8,
	}

	public enum BoardStatus
	{
		Unknown = 0,
		Created = 1, // Before static data has been prepared.
		Calculated = 2, // Basic board constants have been calculated
		Safe = 3,
		Checked = 4,
		DoubleChecked = 5,
		Mated = 6,
		Draw = 7,
	}
}
