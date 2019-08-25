using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SetGame
{
	public class Combination : IEquatable<Combination>
	{
		private readonly Card _a, _b, _c;

		public Combination(Card a, Card b, Card c)
		{
			_a = a;
			_b = b;
			_c = c;

			IsValid = a != b && a != c && b != c
				   && ((_a.Color == _b.Color && _a.Color == _c.Color) || (_a.Color != _b.Color && _a.Color != _c.Color && _b.Color != _c.Color))
				   && ((_a.Shape == _b.Shape && _a.Shape == _c.Shape) || (_a.Shape != _b.Shape && _a.Shape != _c.Shape && _b.Shape != _c.Shape))
				   && ((_a.Fill == _b.Fill && _a.Fill == _c.Fill) || (_a.Fill != _b.Fill && _a.Fill != _c.Fill && _b.Fill != _c.Fill))
				   && ((_a.Count == _b.Count && _a.Count == _c.Count) || (_a.Count != _b.Count && _a.Count != _c.Count && _b.Count != _c.Count));
	}

		public bool Equals(Combination other)
		{
			var self = OrderedCardNumbers();
			var numbers = other.OrderedCardNumbers();
			return self[0] == numbers[0] && self[1] == numbers[1] && self[2] == numbers[2];
		}

		public int[] OrderedCardNumbers()
		{
			var numbers = CardNumbers().ToList();
			numbers.Sort();
			return numbers.ToArray();
		}

		public IEnumerable<int> CardNumbers()
		{
			yield return _a.GetHashCode();
			yield return _b.GetHashCode();
			yield return _c.GetHashCode();
		}

		public bool Contains(Card option)
		{
			return _a == option || _b == option || _c == option;
		}

		public bool IsValid { get; private set; }

		public override string ToString()
		{
			var a = _a.ToString();
			var b = _b.ToString();
			var c = _c.ToString();
			var sb = new StringBuilder();
			var types = new string[] { "Color", "Shape", "Fill", "Count" };

			sb.AppendLine($"\t{_a.GetHashCode()}\t{_b.GetHashCode()}\t{_c.GetHashCode()}");
			for(var i = 0; i < types.Length; i++)
				sb.AppendLine($"{types[i]}\t{a[i]}\t{b[i]}\t{c[i]}");
			return sb.ToString();
		}
	}
}
