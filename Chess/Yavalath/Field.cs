using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yavalath
{
    public class Field
    {
		public const int Sides = 6;
		public const int Left = 0;
		public const int DownLeft = 1;
		public const int DownRight = 2;
		public const int Right = 3;
		public const int UpRight = 4;
		public const int UpLeft = 5;
		public Player Taken { get; set; }
		/// <summary>
		/// Hexagonal fields are placed in clockwise order:
		/// right, down-right, down-left, left, up-left, up-right
		/// 0 1 7 19 37
		/// </summary>
		public Field[] Neighbors { get; } = new Field[Sides];
		
		public void ConnectNeighbors(Board board, int index, int depth)
		{
			if (depth < 4)
			{
				if (Neighbors[Left] == null)
				{
					Neighbors[Left] = board.Fields[index + 1];
				}
			}
			else if (depth == 4)
			{

			}
		}
	}
}
