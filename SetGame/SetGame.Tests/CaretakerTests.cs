using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SetGame.Tests
{
	[TestClass]
	public class CaretakerTests
	{
		[TestMethod]
		public void DefaultSituation_GetState_ProducesObject()
		{
			// Arrange
			var game = new Game();

			// Act
			var state = game.GetState();

			// Assert
			Assert.IsNotNull(state);
		}
	}
}
