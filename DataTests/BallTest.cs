using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Data;

namespace DataTests
{
    [TestClass]
    public class BallTests
    {
        readonly float test_X = 10;
        readonly float test_Y = 10;
        readonly int test_radius = 7;
        readonly int test_id = 1;
        readonly float test_mass  = 10;


        Ball testBall;

        [TestMethod]
        public void ConstructorWithArgumentsTest()
        {
            testBall = new Ball(test_X, test_Y, test_radius, test_id, test_mass);

            Assert.IsNotNull(testBall);
            Assert.AreEqual(test_X, testBall.Position_X);
            Assert.AreEqual(test_Y, testBall.Position_Y);
            Assert.AreEqual(test_radius, testBall.Radius);
            Assert.IsNotNull(testBall.Speed);
            Assert.AreEqual(test_mass, testBall.Mass);
            Assert.AreEqual(test_id, testBall.Id);

        }

        [TestMethod]
        public void ZeroArgumentConstructor()
        {
            testBall = new Ball(test_id);

            Assert.IsNotNull(testBall);
            Assert.AreEqual(10, testBall.Position_X);
            Assert.AreEqual(10, testBall.Position_Y);
            Assert.AreEqual(10, testBall.Radius);
            Assert.IsNotNull(testBall.Speed);
            Assert.AreEqual(10, test_mass);
        }

        [TestMethod]
        public void MoveTest()
        {
            testBall = new Ball(test_X, test_Y, test_radius, test_id, test_mass);

            testBall.Move();

            Assert.AreEqual(10 + testBall.Movement[0], testBall.Position_X);
            Assert.AreEqual(10 + testBall.Movement[1], testBall.Position_Y);

        }

        [TestMethod]
        public void SwitchDirectionTest()
        {
            testBall = new Ball(test_id);

            int x = 1;
            int x2 = 1;
            int y = -1;
            int y2 = -1;
            if (testBall.Movement[0] < 0)
                x = -x;
            if (testBall.Movement[1] < 0)
                x2 = -x;

            testBall.SwitchDirections(true);
            testBall.SwitchDirections(false);

            if (testBall.Movement[0] < 0)
                y = -y;
            if (testBall.Movement[1] < 0)
                y2 = -y2;
        }

        [TestMethod]
        public void RandomizeMovementTest()
        {
            testBall = new Ball(test_id);

            testBall.RandomizeMovement();

            Assert.IsTrue(testBall.Movement[0] >= -1 && testBall.Movement[0] <= 1);
            Assert.IsTrue(testBall.Movement[1] >= -1 && testBall.Movement[1] <= 1);
        }

        
    }
}
