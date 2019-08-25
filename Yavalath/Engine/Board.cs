using System;
using System.Collections.Generic;

namespace Engine
{
	public class Board
	{
		public bool WhiteToMove = true;
		public const int InitialWidth = 5;
		public const int MaxWidth = 9;
		public Field[][] Fields = new Field[MaxWidth][];
		private bool contextInitialized = false;
		private List<Field> whiteFields = new List<Field>();
		private List<Field> blackFields = new List<Field>();
		private List<Tuple<Field, Field>> sameColorPairs = new List<Tuple<Field, Field>>();

		public Board()
		{
			var width = InitialWidth;
			var direction = 1;

			for (byte row = 0; row < Fields.Length; row++)
			{
				Fields[row] = new Field[width];

				for (byte col = 0; col < width; col++)
				{
					Fields[row][col] = new Field(row, col);
				}

				width += direction;
				if (width == MaxWidth)
				{
					direction = -1;
				}
			}
		}

		public string GenerateHexagonalPoints(int x, int y)
		{
			return string.Empty;
		}

		public IEnumerable<Field> AllFields()
		{
			var width = InitialWidth;
			var direction = 1;

			for (byte row = 0; row < Fields.Length; row++)
			{
				for (byte col = 0; col < width; col++)
				{
					yield return Fields[row][col];
				}

				width += direction;
				if (width == MaxWidth)
				{
					direction = -1;
				}
			}
		}

		public void GenerateContext()
		{
			if (contextInitialized)
			{
				return;
			}
			contextInitialized = true;

			ListTakenFields();
			FindSameColorPairs();
		}

		public void ListTakenFields()
		{
			foreach (var field in AllFields())
			{
				if (field.Occupant != null)
				{
					(field.Occupant.IsWhite ? whiteFields : blackFields).Add(field);
				}
			}
		}

		public void FindSameColorPairs()
		{
			for (var i = 0; i < whiteFields.Count; i++)
			{
				var a = whiteFields[i];
				for (var j = i + 1; j < whiteFields.Count; j++)
				{
					var b = whiteFields[j];
					if (a.Occupant.IsWhite == b.Occupant.IsWhite)
					{
						var colDiff = Math.Abs(a.Col - b.Col);
						var rowDiff = Math.Abs(b.Row - b.Row);

						if (colDiff <= 1 && rowDiff <= 1)
						{
							sameColorPairs.Add(new Tuple<Field, Field>(a, b));
						}
					}
				}
			}
		}

		/// <summary>
		/// Change the board by making a move.
		/// </summary>
		/// <param name="move">The move made by a player.</param>
		/// <returns>False if the move is illegal.</returns>
		public bool MakeMove(Move move)
		{
			if (move.Row < 0 || move.Row >= Fields.Length || move.Col < 0 || move.Col >= Fields[move.Row].Length)
			{
				return false;
			}
		}
	}
}
