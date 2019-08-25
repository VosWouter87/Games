using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetGame;
using System.Collections.Generic;
using System.Linq;

namespace SetGame.Tests
{
	[TestClass]
	public class CombinationTests
	{
		private List<Card> deck = Card.AllCards().ToList();

		[TestMethod]
		public void FirstThreeCards_IsValid_ReturnsTrue()
		{
			// Arrange  
			var combination = new Combination(deck[0], deck[1], deck[2]);

			// Act
			var valid = combination.IsValid;

			// Assert
			Assert.IsTrue(valid);
		}

		[TestMethod]
		public void FirstSecondAndFourth_IsValid_ReturnsFalse()
		{
			// Arrange
			var combination = new Combination(deck[0], deck[1], deck[3]);

			// Act
			var valid = combination.IsValid;

			// Assert
			Assert.IsFalse(valid);
		}
	}
}
