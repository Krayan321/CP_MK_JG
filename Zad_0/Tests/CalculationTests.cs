using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Zad_0.Calculations;

namespace Tests
{
    [TestClass]
    public class CalculationTests
    {
        double a = 10;
        double b = 5;
        [TestMethod]
        public void addTest()
        {
            Assert.AreEqual(a + b, add(a, b));
        }

        [TestMethod]
        public void subtractTest()
        {
            Assert.AreEqual(a - b, subtract(a, b));
        }

        [TestMethod]
        public void divideTest()
        {
            Assert.AreEqual(a / b, divide(a, b));
            Assert.AreEqual(0, divide(a, 0));
        }

        [TestMethod]
        public void multiplyTest()
        {
            Assert.AreEqual(a * b, multiply(a, b));
        }
    }
}