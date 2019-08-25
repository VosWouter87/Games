using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yavalath
{
	/// <summary>
	/// Hexagonal fields are placed and calculations are done in clockwise order:
	/// right, down-right, down-left, left, up-left, up-right
	/// </summary>
	public class Board
	{
		public readonly bool WinIf3ButAlso4;
		public const int Size = 61;
		public readonly Field[] Fields = new Field[Size];

		/// <summary>
		/// Make 4 in a row to win, but lose if you make 3 in a row.
		/// </summary>
		/// <param name="winIf3ButAlso4">4 in a row wins the game, but even when that also makes 3 in a row?</param>
		public Board(bool winIf3ButAlso4)
		{
			this.WinIf3ButAlso4 = winIf3ButAlso4;
			var rowLength = 5;
			var totalRows = 9;

			for(var i = 0; i < Size; i++)
			{
				Fields[i] = new Field();
			}

			Fields[0].ConnectNeighbors(this, 0, 0);

			var currentField = 0;
			for(var i = 0; i < totalRows; i++)
			{
				for(var j = 0; j < rowLength; j++)
				{
					if ((j + 1) < rowLength)
					{
						Fields[currentField].Neighbors[0] = Fields[currentField + 1];
					}

					if ((i + 1) < totalRows && 
				}
			}
		}

	}
}
