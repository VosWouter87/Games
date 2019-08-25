using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CantStop
{
	public enum FieldType { Start, Normal, Final };

	public class Field : Button
	{
		public FieldType Type { get; }
		public bool HasMarker { get; set; }
		public List<Color> Squares { get; } = new List<Color>();

		public Field(FieldType type)
		{
			Type = type;
		}

		public void AddColor(Color color)
		{
			if (!Squares.Contains(color))
				Squares.Add(color);
		}

		public void RemoveColor(Color color)
		{
			if (!Squares.Contains(color))
				Squares.Add(color);
		}
	}
}
