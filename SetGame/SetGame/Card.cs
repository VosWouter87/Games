using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SetGame
{
	public class Card : IEquatable<Card>
	{
		public Color Color { get; set; }
		public Shape Shape { get; set; }
		public Fill Fill { get; set; }
		public Count Count { get; set; }
		public Position Position { get; set; }
		public bool Selected { get; set; }
		public Button Button { get; set; }

		// 3 * 3 * 3 * 3, three options in four aspects.
		private const int TotalCards = 81;
		private static List<Card> _deck, _stack, _inPlay, _won;
		private static Random _random = new Random();

		static Card()
		{
			ResetCards();
		}

		public Card(Color color, Shape shape, Fill fill, Count count)
		{
			Color = color;
			Shape = shape;
			Fill = fill;
			Count = count;
			Position = Position.Stack;
		}

		public Card(string symbols)
		{
			switch(symbols[0])
			{
				case 'R': Color = Color.Red; break;
				case 'G': Color = Color.Green; break;
				case 'P': Color = Color.Purple; break;
				default: throw new NotSupportedException("color letter: " + symbols[0]);
			}
			switch (symbols[1])
			{
				case 'E': Shape = Shape.Elipse; break;
				case 'W': Shape = Shape.Wave; break;
				case 'R': Shape = Shape.Rect; break;
				default: throw new NotSupportedException("Shape letter: " + symbols[1]);
			}
			switch (symbols[2])
			{
				case 'E': Fill = Fill.Empty; break;
				case 'G': Fill = Fill.Grain; break;
				case 'F': Fill = Fill.Full; break;
				default: throw new NotSupportedException("Fill letter: " + symbols[2]);
			}
			switch (symbols[3])
			{
				case '1': Count = Count.One; break;
				case '2': Count = Count.Two; break;
				case '3': Count = Count.Three; break;
				default: throw new NotSupportedException("Count letter: " + symbols[3]);
			}
		}

		public override int GetHashCode()
		{
			return CalculateHashCode();
		}

		public int CalculateHashCode()
		{
			return ((int)Color) * 27 + ((int)Shape) * 9 + ((int)Fill) * 3 + ((int)Count) - 1;
		}

		public override string ToString()
		{
			return "" + Color.ToString()[0] + Shape.ToString()[0] + Fill.ToString()[0] + ((int)Count).ToString() + " " + Position + " " + GetHashCode();
		}

		public bool Equals(Card other)
		{
			return this.Color == other.Color
				&& this.Shape == other.Shape
				&& this.Fill == other.Fill
				&& this.Count == other.Count;
		}

		public void ChangeStatusTo(Position newPosition)
		{
			(this.Position == Position.Stack
				? _stack // Deck.
				: this.Position == Position.Play
				? _inPlay // Cards in play.
				: _won // Last option the won set.
			).Remove(this);

			this.Position = newPosition;

			(this.Position == Position.Stack
				? _stack // Deck.
				: this.Position == Position.Play
				? _inPlay // Cards in play.
				: _won // Last option the won set.
			).Add(this);

			if (Position == Position.Play)
			{
				if (this.Button != null)
				{
					this.Button.ShowCardOnButton(this);
				}
			}
		}
		
		public static void ResetCards()
		{
			_deck = AllCards();
			_stack = new List<Card>(_deck);
			_inPlay = new List<Card>();
			_won = new List<Card>();
		}

		public static List<Card> AllCards()
		{
			var cards = new List<Card>(TotalCards);

			foreach (var color in Enum.GetValues(typeof(Color)))
				foreach (var shape in Enum.GetValues(typeof(Shape)))
					foreach (var fill in Enum.GetValues(typeof(Fill)))
						foreach (var count in Enum.GetValues(typeof(Count)))
							cards.Add(new Card((Color)color, (Shape)shape, (Fill)fill, (Count)count));

			return cards;
		}

		public static List<Card> CardsInPlay()
		{
			return new List<Card>(_inPlay);
		}

		public static Card DrawRandomCard()
		{
			if (_stack.Count == 0)
			{
				throw new InvalidOperationException("Cannot draw from an empty deck!");
			}

			var index = _random.Next(_stack.Count);
			var card = _stack[index];
			_stack.RemoveAt(index);
			return card;
		}

		public static Card FindCard(int hashCode)
		{
			return _deck[hashCode];
		}
	}
}