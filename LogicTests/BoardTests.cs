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

        [TestMethod]
        public void MoveBallsTest()
        {
            testBoard = new Board(testWidth, testHeight);
            testBoard.AddBall(testBall);

            testBall.Position_X = testWidth / 2;
            testBall.Position_Y = testHeight / 2;

            float position_X = testBall.Position_X;
            float position_Y = testBall.Position_Y;

            testBoard.MoveBalls();

            Assert.IsTrue(position_X != testBall.Position_X);
            Assert.IsTrue(position_Y != testBall.Position_Y);
        }

    }
}
