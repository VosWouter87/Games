using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetGame
{
	public static class Extensions
	{
		public static void ShowCardOnButton(this Button button, Card card)
		{
			var color = System.Drawing.Color.FromName(card.Color.ToString());
			// Reset button looks:
			button.ForeColor = System.Drawing.Color.Black;
			button.BackColor = System.Drawing.Color.White;
			button.Font = new Font(button.Font, FontStyle.Regular);
			button.FlatStyle = FlatStyle.Standard;

			switch (card.Fill)
			{
				case Fill.Empty:
					button.FlatStyle = FlatStyle.Flat;
					button.FlatAppearance.BorderColor = color;
					button.FlatAppearance.BorderSize = 3;
					break;
				case Fill.Grain:
					button.ForeColor = color;
					button.Font = new Font(button.Font, FontStyle.Bold);
					break;
				case Fill.Full:
					button.BackColor = color;
					button.ForeColor = System.Drawing.Color.White;
					break;
			}

			var shape = card.Shape.ToString();

			button.Text = string.Empty;
			for (var i = 0; i < (int)card.Count; i++)
			{
				button.Text += shape + Environment.NewLine;
			}

			button.Tag = card;
			card.Button = button;
		}
    }
}
