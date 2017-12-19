using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class KundeUpdateTest
    {
        private KundeManager target;
        private KundeManager Target => target ?? (target = new KundeManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Kunde kunde = target.FindKundeById(1);
            string nameBefore = kunde.Nachname;
            kunde.Nachname = "Chabis";
            target.Update(kunde);
            kunde = target.FindKundeById(1);
            Assert.AreNotEqual(nameBefore, kunde.Nachname);
        }
    }
}
