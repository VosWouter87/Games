using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Engine.Test
{
	[TestClass]
	public class BoardTest
	{
		[TestMethod]
		public void DefaultPosition_CalculateMoves_OnlyPawnsAndKnightsCanMove()
		{
			// Arrange
			var expected = new List<int>(new int[] { 5117064, 5116040, 5119664, 5118640, 5247105, 5247169, 5247625, 5247689, 5248145, 5248209, 5248665, 5248729, 5249185, 5249249, 5249705, 5249769, 5250225, 5250289, 5250745, 5250809 });
			var board = new Board(Board.Default);

			// Act
			var moves = board.CalculateMoves();

			// Assert
			Assert.AreEqual(expected.Count, moves.Count);
			for (var i = 0; i < moves.Count; i++)
			{
				Assert.AreEqual(expected[i], moves[i].ToInt());
			}
		}

		[TestMethod]
		public void DynamicPosition_CalculateMoves_MoveSet()
		{
			// Arrange
			var expected = new List<int>(new int[] { 6425834, 6426410, 6425706, 6424682, 6424106, 7211355, 7210331, 7209755, 7210075, 7217819, 7211099, 7408364, 7407916, 7407404, 7415084, 7408492, 7408556, 7408620, 7408876, 7409324, 7407980, 7407532, 7407084, 7342965, 7342453, 7343030, 7342518, 7475135, 7475071, 7475007, 7474943, 7474879, 7474815, 7474687, 7474175, 7473663, 7473151, 7472639, 7480319 });
			var board = new Board(@"k6r/p3ppp1/N1b1q3/3n4/2Pp4/4B1P1PP3PKP/1R3Q2 b k c3 2 35");

			// Act
			var moves = board.CalculateMoves();

			// Assert
			Assert.AreEqual(expected.Count, moves.Count);
			for (var i = 0; i < moves.Count; i++)
			{
				Assert.AreEqual(expected[i], moves[i].ToInt());
			}
		}

		[TestMethod]
		public void KingUnderThreat_CalculateMoves_MoveSet()
		{
			// Arrange
			var expected = new List<int>(new int[] { 4936981, 4936533 });
			var board = new Board(@"rnb2rk1/ppp3pp/4b3/q1pN4/7n/1P1P1K1P/PBP1P1BP/1R1Q4 w - - 3 25");

			// Act
			var moves = board.CalculateMoves();

			// Assert
			Assert.AreEqual(expected.Count, moves.Count);
			for (var i = 0; i < moves.Count; i++)
			{
				Assert.AreEqual(expected[i], moves[i].ToInt());
			}
		}
	}
}
