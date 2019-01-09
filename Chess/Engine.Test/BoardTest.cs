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
			var expected = new List<int>(new int[] {  });
#pragma warning disable CRRSP01 // A misspelled word has been found
			var board = new Board(@"k5r1/p2bppp1/N2pq3/4p3/2Pn4/4B3/P1P2PPP/1R3QK1 b - - 2 35");
#pragma warning restore CRRSP01 // A misspelled word has been found

			// Act
			var moves = board.CalculateMoves();

			// Assert
			expected.Clear();
			//Assert.AreEqual(expected.Count, moves.Count);
			for (var i = 0; i < moves.Count; i++)
			{
				expected.Add(moves[i].ToInt());
				//Assert.AreEqual(expected[i], moves[i].ToInt());
			}
			Assert.AreEqual(20, expected.Count);
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
