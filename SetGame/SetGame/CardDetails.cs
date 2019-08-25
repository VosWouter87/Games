using System;
using System.Windows.Forms;

namespace SetGame
{
	public partial class CardDetails : Form
	{
		private Button _button;
		private Game _game;

		/// <summary>
		/// Open a details window to change the properties of a card.
		/// </summary>
		/// <param name="card">Leave null if making a new card.</param>
		public CardDetails(Button button, Game game)
		{
			_button = button;
			_game = game;
		
			InitializeComponent();
			InitializeDynamic();
			this.Show();
		}

		private void ButtonOk_Click(object sender, EventArgs e)
		{
			var card = Card.FindCard(new Card(
				(Color)comboBoxColor.SelectedValue,
				(Shape)comboBoxShape.SelectedValue,
				(Fill)comboBoxFill.SelectedValue,
				(Count)comboBoxCount.SelectedValue).GetHashCode());

			Console.WriteLine(card);

			// Ensure existing card is kept around.
			if (_button.Tag is Card existing)
			{
				_game.RemoveCardFromPlay(existing, false);
			}

			_game.AddCardToEnd(card);

			this.Close();
		}

		private void ButtonDelete_Click(object sender, EventArgs e)
		{
			_button.Parent.Controls.Remove(_button);
			_game.RemoveCardFromPlay((Card)_button.Tag, true);
			this.Close();
		}
	}
}
