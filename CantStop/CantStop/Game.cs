using System;

namespace CantStop
{
	public class Game
	{
		public int NumberOfPlayers { get; }
		public bool AllowStacking { get; }
		public Random die = new Random();

		public Game(int numberOfPlayers, bool allowStacking)
		{
			NumberOfPlayers = numberOfPlayers;
			AllowStacking = allowStacking;
		}

		public int[] Roll()
		{
			return new int[] { die.Next(6) + 1, die.Next(6) + 1, die.Next(6) + 1, die.Next(6) + 1 };
		}
	}
}
