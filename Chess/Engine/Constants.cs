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
		WhiteKingSide = 0,
		WhiteQueenSide = 1,
		BlackKingSide = 2,
		BlackQueenSide = 4,
	}

	public enum KingStatus
	{
		Unknown = 0,
		Safe = 1,
		Checked = 2,
		Mated = 3,
		Draw = 4,
	}
}
