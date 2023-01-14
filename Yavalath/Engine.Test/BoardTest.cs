using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Engine.Test
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void GenerateHexagonalPoints()
        {
			var points = Board.GenerateHexagonalPoints(0, 30);
			Debug.WriteLine(points);
			Assert.IsTrue(points.Length > 0);
        }
    }
}
