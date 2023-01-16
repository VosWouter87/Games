using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CantStop
{
	public class Line : Button
	{
		public int Marker { get; set; } = 0;
		public List<int> Traffic { get; } = new List<int>();

    public Line (int playerCount) {
      for (var i = 0; i < playerCount; i++) {
        Traffic.Add(0);
      }
    }

    protected override void OnPaint (System.Windows.Forms.PaintEventArgs e) {
      GraphicsPath grPath = new GraphicsPath();
      grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
      this.Region = new System.Drawing.Region(grPath);
      base.OnPaint(e);
    }
  }
}
