using System.Collections.Generic;
using System.Linq;
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
			var expected = new List<int>(new int[] { 5248008, 5248520, 5117057, 5116929, 5248073, 5248585, 5248138, 5248650, 5248203, 5248715, 5248268, 5248780, 5248333, 5248845, 5117382, 5117254, 5248398, 5248910, 5248463, 5248975, });
			var board = new Board(Board.Default);

			// Act
			var moves = board.CalculateMoves();

			// Assert
			AssertListsAreEqual(expected, moves.Select(m => m.ToInt()).ToList());
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
			AssertListsAreEqual(expected, moves.Select(m => m.ToInt()).ToList());
		}

		[TestMethod]
		public void KingUnderThreat_CalculateMoves_MoveSet()
		{
			// Arrange
			var board = new Board(@"rnb2rk1/ppp3pp/3b4/q1pN4/7n/1P1P1K1P/PBP1P1BP/1R1Q4 w - - 3 25");
			var expected = new List<Move>();
			board.Fields[5, 2].Piece.MakeMove(board.Fields[4, 2], expected);
			board.Fields[5, 2].Piece.MakeMove(board.Fields[4, 3], expected);

			// Act
			var moves = board.CalculateMoves();

			// Assert
			AssertListsAreEqual(expected, moves);
		}

		private void AssertListsAreEqual(List<Move> expected, List<Move> actual)
		{
			AssertListsAreEqual(expected.Select(m => m.ToInt()).ToList(), actual.Select(m => m.ToInt()).ToList());
		}

		private void AssertListsAreEqual(List<int> expected, List<int> actual)
		{
			Assert.AreEqual(expected.Count, actual.Count);
			expected.Sort();
			actual.Sort();
			for(var i = 0; i < expected.Count; i++)
			{
				Assert.AreEqual(
					expected[i],
					actual[i],
					$"Expected move: { new Move(expected[i]) }, actually got move: { new Move(actual[i]) } on index: { i }");
			}
		}
	}
}
