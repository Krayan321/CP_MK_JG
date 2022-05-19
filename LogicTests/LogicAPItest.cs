using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Logic;
using static Logic.LogicAPI;

namespace LogicTests
{

    [TestClass]
    public class LogicAPItest
    {
        TestDataAPI testDataAPI = new TestDataAPI();
        
        
        [TestMethod]
        public void AddBallTest()
        {
            LogicAPI Logic = new BusinessLogic(testDataAPI, true);
            Assert.AreEqual(Logic.AddBall(), 0);

            Assert.AreEqual(testDataAPI.GetBallsCount(), Logic.GetBallsCount());
        }
    }
}
