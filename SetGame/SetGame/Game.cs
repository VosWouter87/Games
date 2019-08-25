using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SetGame
{
	public delegate void CombinationCountChanged(int options);
	public delegate void UpdateCards();

	public class Game
	{
		private JsonSerializer _serializer = JsonSerializer.Create();
		private JsonTextWriter _textWriter = new JsonTextWriter(new StringWriter());

		#region Memento
		public GameState GetState()
		{
			return new GameState(
				_inPlay.Select(c => c.GetHashCode()).ToArray(),
				_cardsWon.Select(c => c.GetHashCode()).ToArray());
		}

		public void SetState(GameState gameState)
		{
			if (gameState != null)
			{
				// Reset current gamestate.
				Card.ResetCards();

				var removedFromDeck = new List<int>(gameState.InPlay.Length + gameState.Won.Length);

				for (var i = 0; i < gameState.InPlay.Length; i++)
				{
					Card.FindCard(gameState.InPlay[i]).ChangeStatusTo(Position.Play);
				}

				for (var i = 0; i < gameState.Won.Length; i++)
				{
					Card.FindCard(gameState.Won[i]).ChangeStatusTo(Position.Won);
				}

				UpdateCards?.Invoke();
				CalculateOptionsForAllCards();
			}
		}
		#endregion

		private IList<Combination> _combinations = new List<Combination>();
		private IList<Card> _inPlay = new List<Card>();
		private List<Card> _deck = Card.AllCards();
		private IList<Card> _cardsWon = new List<Card>();
		
		public CombinationCountChanged CombinationCountChanged;
		public UpdateCards UpdateCards;
		
		public List<Combination> Options
		{
			get
			{
				return _combinations.ToList();
			}
		}

		public bool PutCardInPlay(Card card)
		{
			if (card.Position == Position.Play)
			{
				return false;
			}
			
			_inPlay.Add(card);

			return true;
		}
		
		public void SelectCard(Card selected)
		{
		}
		
		public void RemoveCardFromPlay(Card card, bool playerWonCard)
		{
			card.ChangeStatusTo(playerWonCard ? Position.Won : Position.Stack);
			UpdateCards?.Invoke();

			for (var i = 0; i < _combinations.Count; i++)
			{
				if (_combinations[i].Contains(card))
				{
					_combinations.RemoveAt(i);
					i--;
				}
			}

			CombinationCountChanged?.Invoke(_combinations.Count);
		}

		public void CalculateOptionsForAllCards()
		{
			for (var a = 0; a < _inPlay.Count; a++)
				for (var b = a + 1; b < _inPlay.Count; b++)
					for (var c = b + 1; c < _inPlay.Count; c++)
					{
						var candidate = new Combination(_inPlay[a], _inPlay[b], _inPlay[c]);
						if (candidate.IsValid && !_combinations.Contains(candidate))
						{
							_combinations.Add(candidate);
						}
					}

			CombinationCountChanged?.Invoke(_combinations.Count);
		}
		
		public void AddCardToEnd(Card card)
		{
			CalculateOptions(card);
			card.ChangeStatusTo(Position.Play);
			UpdateCards?.Invoke();
		}

		public void CalculateOptions(Card card)
		{
			foreach (var b in _inPlay)
			{
				foreach (var c in _inPlay)
				{
					if (b == c) break;

					var candidate = new Combination(card, b, c);
					if (candidate.IsValid && !_combinations.Contains(candidate))
					{
						_combinations.Add(candidate);
					}
				}
			}

			CombinationCountChanged?.Invoke(_combinations.Count);
		}

		public string GetJsonState()
		{
			_serializer.Serialize(this._textWriter, this.GetState());
			return this._textWriter.ToString();
		}

		public void SetJsonState(string json)
		{
			var textReader = new JsonTextReader(new StringReader(json));
			var state = _serializer.Deserialize(textReader, typeof(GameState));

			if (state is GameState gameState)
			this.SetState(gameState);
		}
	}
}
