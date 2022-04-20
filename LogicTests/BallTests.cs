using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Logic;

namespace LogicTests
{
    [TestClass]
    public class BallTests
    {
        readonly float test_X = 5;
        readonly float test_Y = 10;
        readonly int test_radius = 7;

        Ball testBall;

        [TestMethod]
        public void ConstructorWithArgumentsTest()
        {
            testBall = new Ball(test_X, test_Y, test_radius);

            Assert.IsNotNull(testBall);
            Assert.AreEqual(test_X, testBall.Position_X);
            Assert.AreEqual(test_Y, testBall.Position_Y);
            Assert.AreEqual(test_radius, testBall.Radius);
            Assert.IsNotNull(testBall.Speed);
            Assert.IsNotNull(testBall.Direction);
        }

        [TestMethod]
        public void ZeroArgumentConstructor()
        {
            testBall = new Ball();

            Assert.IsNotNull(testBall);
            Assert.AreEqual(10, testBall.Position_X);
            Assert.AreEqual(10, testBall.Position_Y);
            Assert.AreEqual(10, testBall.Radius);
            Assert.IsNotNull(testBall.Speed);
            Assert.IsNotNull(testBall.Direction);
        }

        [TestMethod]
        public void MoveTest()
        {
            testBall = new Ball();

            testBall.Move();

            Assert.AreEqual(10 + testBall.Direction[0] * testBall.Speed, testBall.Position_X);
            Assert.AreEqual(10 + testBall.Direction[1] * testBall.Speed, testBall.Position_Y);
        }

        [TestMethod]
        public void RandomizePositionTest()
        {
            testBall = new Ball();
            testBall.RandomizePosition(50, 50);

            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(testBall.Position_X >= 10 && testBall.Position_X <= 50 - 10);
                Assert.IsTrue(testBall.Position_Y >= 10 && testBall.Position_X <= 50 - 10);
            }
            
        }
    }
}
