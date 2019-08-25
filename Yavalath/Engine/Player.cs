using System.Drawing;

namespace Engine
{
	public class Player
	{
		public readonly Color Color;
		public readonly bool IsWhite;

		public Player(Color color, bool isWhite)
		{
			Color = color;
			IsWhite = isWhite;
		}
	}
}
