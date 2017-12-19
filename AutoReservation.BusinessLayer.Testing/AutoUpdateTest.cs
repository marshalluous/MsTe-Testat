using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class AutoUpdateTests
    {
        private AutoManager target;
        private AutoManager Target => target ?? (target = new AutoManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            Auto auto = Target.FindAutoById(1);
            string markeBefore = auto.Marke;
            auto.Marke = "Mitsubishi Lancer";
            Target.Update(auto);
            auto = target.FindAutoById(1);

            Assert.AreNotSame(markeBefore, auto.Marke);
        }
    }
}
