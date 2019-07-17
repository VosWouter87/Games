using System.Drawing;
using System.Text;

namespace Engine
{
    public class Board
    {
		public const int a = 30, b = 20, c = 36;
		public static readonly Point[] CornerDirections = new[] { new Point(a, b), new Point(c, 0), new Point(a, -b), new Point(-a, -b), new Point(-c, 0), new Point(-a, b) };

		public static string GenerateHexagonalPoints(int x, int y)
		{
			var points = new StringBuilder("" + x + "," + y);

			foreach(var c in CornerDirections)
			{
				x += c.X;
				y += c.Y;
				points.Append(" " + x + "," + y);
			}

			return points.ToString();
		}
    }
}
