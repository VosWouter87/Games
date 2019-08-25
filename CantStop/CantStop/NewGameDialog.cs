using System;
using System.Windows.Forms;

namespace CantStop
{
	public partial class NewGameDialog : Form
	{
		public NewGameDialog()
		{
			InitializeComponent();
			this.buttonPlay.Click += new System.EventHandler(this.ButtonPlay_Click);
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
		}

		public void ButtonPlay_Click(object sender, EventArgs e)
		{
			int playerCount = 0;

			if (radio2Players.Checked)
				playerCount = 2;
			else if (radio3Players.Checked)
				playerCount = 3;
			else if (radio4Players.Checked)
				playerCount = 4;
			else
				throw new Exception("Invalid selected player count.");

			var game = new Game(playerCount, checkAllowStacking.Checked);
			CantStop.Instance.StartGame(game);
			ButtonCancel_Click(null, null);
		}

		public void ButtonCancel_Click(object sender, EventArgs e)
		{
			Close();
			Dispose();
		}
	}
}
