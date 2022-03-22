using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CalculatorTests
    {
        Zad_0.Calculator calculator = new Zad_0.Calculator();
        double a = 10;
        double b = 5;
        [TestMethod]
        public void addTest()
        {
            Assert.AreEqual(a + b, calculator.add(a, b));
        }

        [TestMethod]
        public void subtractTest()
        {
            Assert.AreEqual(a - b, calculator.subtract(a, b));
        }

        [TestMethod]
        public void divideTest()
        {
            Assert.AreEqual(a / b, calculator.divide(a, b));
            Assert.AreEqual(0, calculator.divide(a, 0));
        }

        [TestMethod]
        public void multiplyTest()
        {
            Assert.AreEqual(a * b, calculator.multiply(a, b));
        }
    }
}