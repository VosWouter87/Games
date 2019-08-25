using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SetGame
{
	public partial class View : Form
	{
		private Game _game = new Game();
		private List<Button> _cards = new List<Button>();
		private Mode GameMode = Mode.Play;
		private int rowCount = 3;

		public View()
		{
			InitializeComponent();
			_game.CombinationCountChanged += CombinationCountChanged;
			_game.UpdateCards += UpdateCards;
		}

		private void RedrawCards()
		{
			foreach (var card in _cards)
			{
				this.tabBoard.Controls.Remove(card);
			}

			var cards = Card.CardsInPlay();
			for (var i = 0; i < cards.Count; i++)
			{
				var column = i / this.rowCount;
				var row = i % this.rowCount;
				var card = new Button();
				card.Location = new Point(column * 55 + 30, row * 55 + 50);
				card.Size = new Size(50, 50);
				card.Click += Card_Click;
				card.ShowCardOnButton(cards[i]);
				this.tabBoard.Controls.Add(card);
				_cards.Add(card);
			}
		}

		private void Card_Click(object sender, EventArgs e)
		{
			switch (GameMode)
			{
				case Mode.Play:
					var button = (Button)sender;
					if (button.Tag is Card card)
					{
						card.Selected = !card.Selected;
						_game.SelectCard(card);
						button.ShowCardOnButton(card);
					}
					else
					{
						throw new Exception("Button doesn't have a card.");
					}
					break;

				case Mode.Edit:
					new CardDetails((Button)sender, _game);
					break;
			}
		}

		private void TabEdit_GotFocus(object sender, System.EventArgs e)
		{
			this.richTextBoxCards.Text = _game.GetJsonState();
		}

		private void TabBoard_GotFocus(object sender, System.EventArgs e)
		{
			throw new System.NotImplementedException();
		}

		private void AddToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Card.DrawRandomCard().ChangeStatusTo(Position.Play);
			Card.DrawRandomCard().ChangeStatusTo(Position.Play);
			Card.DrawRandomCard().ChangeStatusTo(Position.Play);
			_game.CalculateOptionsForAllCards();
			RedrawCards();
		}

		private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var sb = new StringBuilder();
			foreach (var combination in _game.Options)
			{
				sb.AppendLine(combination.ToString());
			}

			MessageBox.Show(sb.ToString());
		}
		
		private void CombinationCountChanged(int count)
		{
			this.Text = "Set " + count.ToString();
			RedrawCards();
		}

		private void UpdateCards()
		{
			RedrawCards();
		}

		private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_game.SetState(new Caretaker().LoadState());
		}

		private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new Caretaker().SaveState(_game.GetState());
		}
	}
}
