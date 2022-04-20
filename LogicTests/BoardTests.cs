using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogicTests
{
    [TestClass]
    public class BoardTests
    {
        readonly int testWidth = 100;
        readonly int testHeight = 100;
        readonly int numberOfBalls = 15;

        Board testBoard;
        readonly Ball testBall = new Ball();

        [TestMethod]
        public void NoBallsConstructor()
        {
            testBoard = new Board(testWidth, testHeight);

            Assert.IsNotNull(testBoard);
            Assert.AreEqual(testWidth, testBoard.Width);
            Assert.AreEqual(testHeight, testBoard.Height);  
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
        public void AddRemoveBallTest()
        {
            testBoard = new Board(testWidth, testHeight);

            testBoard.AddBall(testBall);

            Assert.AreEqual(testBoard.Balls.Count, 1);
            Assert.AreEqual(testBoard.Balls[0], testBall);

            testBoard.RemoveBall(testBall);

            Assert.AreEqual(testBoard.Balls.Count, 0);
        }
    }
}
