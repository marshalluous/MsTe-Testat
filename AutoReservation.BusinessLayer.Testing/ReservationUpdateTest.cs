using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationUpdateTest
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Reservation res = target.FindReservationByNr(1);
            DateTime oldBisDate = res.Bis;
            res.Bis = new DateTime(2020, 07, 07);
            target.Update(res);
            res = target.FindReservationByNr(1);
            Assert.AreNotEqual(oldBisDate, res.Bis);
        }
    }
}
