using System;

namespace SetGame
{
	[Serializable]
	public class GameState
	{
		public readonly int[] InPlay;
		public readonly int[] Won;

		public GameState(int[] inPlay, int[] won)
		{
			InPlay = inPlay;
			Won = won;
		}
	}
}
