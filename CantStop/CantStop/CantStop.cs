using System;
using System.Windows.Forms;

namespace CantStop
{
	public partial class CantStop : Form
	{
		public static readonly CantStop Instance = new CantStop();
		private Game _game;

		public CantStop()
		{
			InitializeComponent();
		}

		private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var newGameDialog = new NewGameDialog();
			newGameDialog.ShowDialog();
		}

		public void StartGame(Game game)
		{
			_game = game;
		}
	}
}
