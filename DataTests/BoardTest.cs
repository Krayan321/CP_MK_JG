using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Data;

namespace DataTests
{
    [TestClass]
    public class BoardTests
    {
        readonly int testWidth = 100;
        readonly int testHeight = 100;
        readonly int numberOfBalls = 15;

        Board testBoard;
        readonly Ball testBall = new Ball(1);

        [TestMethod]
        public void NoBallsConstructor()
        {
            testBoard = new Board(testWidth, testHeight);
            testBoard.Size = new int[] { testWidth, testHeight };
            int[] expectedSize = testBoard.Size;

            Assert.IsNotNull(testBoard);
            CollectionAssert.AreEqual(expectedSize, testBoard.Size);
            
        }

        [TestMethod]
        public void BallsConstructor()
        {
            testBoard = new Board(testWidth, testHeight, numberOfBalls);

            Assert.AreEqual(testBoard.Balls.Count, numberOfBalls);

            foreach (Ball ball in testBoard.Balls)
            {
                Assert.IsNotNull(ball);
            }
        }

        [TestMethod]
        public void AddBallsTest()
        {
            testBoard = new Board(testWidth, testHeight);

            testBoard.AddBall();

            Assert.AreEqual(testBoard.Balls.Count, 1);
            


        }

        

    }
}
