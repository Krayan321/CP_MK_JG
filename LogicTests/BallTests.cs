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

            testBall.Move(900, 900);

            Assert.AreEqual(10 + testBall.Direction[0] * testBall.Speed, testBall.Position_X);
            Assert.AreEqual(10 + testBall.Direction[1] * testBall.Speed, testBall.Position_Y);
        }

        [TestMethod]
        public void RandomizePositionTest()
        {
            testBall = new Ball();
            int maxNumber = 23;

            for (int i = 0; i < 10000; i++)
            {
                testBall.RandomizePosition(maxNumber, maxNumber);
                Assert.IsTrue(testBall.Position_X >= 10 && testBall.Position_X <= maxNumber - 10);
                Assert.IsTrue(testBall.Position_Y >= 10 && testBall.Position_X <= maxNumber - 10);
            }
        }

        [TestMethod]
        public void SwitchDirectionTest()
        {
            testBall = new Ball();

            int x = 1;
            int x2 = 1;
            int y = -1;
            int y2 = -1;
            if (testBall.Direction[0] < 0)
                x = -x;
            if (testBall.Direction[1] < 0)
                x2 = -x;

            testBall.SwitchDirections(true);
            testBall.SwitchDirections(false);

            if (testBall.Direction[0] < 0)
                y = -y;
            if (testBall.Direction[1] < 0)
                y2 = -y2;
        }

        [TestMethod]
        public void RandomizeMovementTest()
        {
            testBall = new Ball();
            
            testBall.RandomizeMovement();

            Assert.IsTrue(testBall.Speed >= 1 && testBall.Speed <= 10);

            Assert.IsTrue(testBall.Direction[0] >= -1 && testBall.Direction[0] <= 1);
            Assert.IsTrue(testBall.Direction[1] >= -1 && testBall.Direction[1] <= 1);
        }
    }
}
