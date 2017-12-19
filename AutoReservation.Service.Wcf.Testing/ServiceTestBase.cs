using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
//using AutoReservation.Common.DataTransferObjects.Faults;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region Read all entities

        [TestMethod]
        public void GetAutosTest()
        {
            IEnumerable<AutoDto> autoDtos = Target.ReadAllAuto();
            Assert.AreEqual(3, autoDtos.Count());
        }

        [TestMethod]
        public void GetKundenTest()
        {
            IEnumerable<KundeDto> kundeDtos = Target.ReadAllKunde();
            Assert.AreEqual(4, kundeDtos.Count());
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            IEnumerable<ReservationDto> resercationDtos = Target.ReadAllReservation();
            Assert.AreEqual(3, resercationDtos.Count());
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetAutoByIdTest()
        {
            AutoDto auto = Target.FindAutoById(3);
            Assert.AreEqual("Audi S6", auto.Marke);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            KundeDto kunde = Target.FindKundeById(3);
            Assert.AreEqual("Martha", kunde.Vorname);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            ReservationDto reservation = Target.FindReservationByNr(2);
            Assert.AreEqual(2, reservation.ReservationsNr);
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetAutoByIdWithIllegalIdTest()
        {
            Assert.IsNull(Target.FindAutoById(0));
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
            Assert.IsNull(Target.FindKundeById(0));
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
            Assert.IsNull(Target.FindReservationByNr(0));
        }

        #endregion

        #region Insert
        [TestMethod]
        public void InsertAutoTest()
        {
            AutoDto auto = new AutoDto
            {
                AutoKlasse = AutoKlasse.Standard,
                Marke = "Hyunday Coupe",
                Tagestarif = 20
            };
            AutoDto insertedAuto = Target.InsertAuto(auto);

            Assert.AreEqual(auto.Marke, insertedAuto.Marke);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            KundeDto kunde = new KundeDto
            {
                Geburtsdatum = new DateTime(1997, 11, 12),
                Nachname = "Mueller",
                Vorname = "Hans"
            };
            KundeDto insertedKunde = Target.InsertKunde(kunde);
        
            Assert.AreEqual(kunde.Geburtsdatum, insertedKunde.Geburtsdatum);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            ReservationDto res = new ReservationDto
            {
                Auto = Target.FindAutoById(2),
                Kunde = Target.FindKundeById(1),
                Von = new DateTime(2017, 12, 18),
                Bis = new DateTime(2017, 12, 19)
            };
            ReservationDto insertedRes = Target.InsertReservation(res);
            Assert.AreEqual(res.Bis, insertedRes.Bis);
        }
        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteAutoTest()
        {
            Target.DeleteAuto(Target.FindAutoById(1));
            Assert.IsNull(Target.FindAutoById(1));
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            Target.DeleteKunde(Target.FindKundeById(1));
            Assert.IsNull(Target.FindKundeById(1));
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            ReservationDto res = Target.FindReservationByNr(1);
            Assert.IsNotNull(res);
            Target.DeleteReservation(res);
            Assert.IsNull(Target.FindReservationByNr(1));
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateAutoTest()
        {
            AutoDto auto = Target.FindAutoById(1);
            auto.Marke = "Subaru Impreza";
            Target.UpdateAuto(auto);
            Assert.AreEqual(auto.Marke, Target.FindAutoById(1).Marke);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            KundeDto kunde = Target.FindKundeById(1);
            kunde.Nachname = "Wurst";
            Target.UpdateKunde(kunde);
            Assert.AreEqual(kunde.Nachname, Target.FindKundeById(1).Nachname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            ReservationDto res = Target.FindReservationByNr(1);
            res.Kunde = Target.FindKundeById(2);
            Target.UpdateReservation(res);
            Assert.AreEqual(2, Target.FindReservationByNr(1).ConvertToEntity().KundeId);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        [ExpectedException(typeof(FaultException<AutoDto>))]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            AutoDto auto1 = Target.FindAutoById(1);
            AutoDto auto2 = Target.FindAutoById(1);
            auto1.Tagestarif = 20;
            auto2.Marke = "Mitsubishi Lancer";

            Target.UpdateAuto(auto2);
            Target.UpdateAuto(auto1);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<KundeDto>))]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            KundeDto kunde1 = Target.FindKundeById(1);
            KundeDto kunde2 = Target.FindKundeById(1);
            kunde1.Nachname = "Hengscht";
            kunde2.Nachname = "Sauber";

            Target.UpdateKunde(kunde2);
            Target.UpdateKunde(kunde1);

        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ReservationDto>))]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            ReservationDto reservation1 = Target.FindReservationByNr(1);
            ReservationDto reservation2 = Target.FindReservationByNr(1);
            reservation1.Von = new DateTime(2021, 11, 11);
            reservation2.Von = new DateTime(2022, 11, 11);

            Target.UpdateReservation(reservation2);
            Target.UpdateReservation(reservation1);
        }

        #endregion

        #region Insert / update invalid time range

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            ReservationDto res = new ReservationDto
            {
                Auto = Target.FindAutoById(1),
                Kunde = Target.FindKundeById(1),
                Von = DateTime.Now,
                Bis = new DateTime(2021, 13, 22)
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            ReservationDto res = Target.FindReservationByNr(1);
            res.Bis = new DateTime(2020,13,13);
            Target.UpdateReservation(res);
        }

        [TestMethod]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Check Availability

        [TestMethod]
        public void CheckAvailabilityIsTrueTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void CheckAvailabilityIsFalseTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion
    }
}
