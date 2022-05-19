using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Data;

namespace DataTests
{
    [TestClass]
    public class DataAPITest
    {

        DataAPI api = DataAPI.CreateDataBase(100, 100);
       
        [TestMethod]
        public void AddBallsTest()
        {
            api.AddBall();

            Assert.AreEqual(api.GetBallsCount(), 1);
        }

        
    }
}
