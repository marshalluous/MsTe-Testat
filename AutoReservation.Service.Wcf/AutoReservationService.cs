using AutoReservation.Common.Interfaces;
using System;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using AutoReservation.BusinessLayer;
using AutoReservation.BusinessLayer.Exceptions;
using System.ServiceModel;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {

        private static void WriteActualMethod() 
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        #region delete
        public AutoDto DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            return new AutoManager().Delete(auto.ConvertToEntity()).ConvertToDto();
        }

        public KundeDto DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            return new KundeManager().Delete(kunde.ConvertToEntity()).ConvertToDto();
        }

        public ReservationDto DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            return new ReservationManager().Delete(reservation.ConvertToEntity()).ConvertToDto();
        }
        #endregion

        #region find
        public AutoDto FindAutoById(int id)
        {
            WriteActualMethod();
            return new AutoManager().FindAutoById(id).ConvertToDto();
        }

        public KundeDto FindKundeById(int id)
        {
            WriteActualMethod();
            return new KundeManager().FindKundeById(id).ConvertToDto();
        }

        public ReservationDto FindReservationByNr(int id)
        {
            WriteActualMethod();
            return new ReservationManager().FindReservationByNr(id).ConvertToDto();
        }
        #endregion

        #region insert
        public AutoDto InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            AutoManager am = new AutoManager();
            return am.Insert(auto.ConvertToEntity()).ConvertToDto();
        }

        public KundeDto InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            KundeManager km = new KundeManager();
            return km.Insert(kunde.ConvertToEntity()).ConvertToDto();
        }

        public ReservationDto InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            if (reservation.Bis - reservation.Von < TimeSpan.FromHours(24))
            {
                throw new InvalidDateRangeException();
            }
            if (!isAvailable(reservation.Auto))
            {
                throw new AutoUnavailableException();
            }
            ReservationManager rm = new ReservationManager();
            return rm.Insert(reservation.ConvertToEntity()).ConvertToDto();
            
        }
        #endregion

        #region availability
        public bool isAvailable(AutoDto auto)
        {
            WriteActualMethod();
            return true;
                //throw new NotImplementedException();
        }
        #endregion

        #region getAll
        public IEnumerable<AutoDto> ReadAllAuto()
        {
            WriteActualMethod();
            return new AutoManager().GetAllAutos().ConvertToDtos();
        }

        public IEnumerable<KundeDto> ReadAllKunde()
        {
            WriteActualMethod();
            return new KundeManager().GetAllKunde().ConvertToDtos();
        }

        public IEnumerable<ReservationDto> ReadAllReservation()
        {
            WriteActualMethod();
            return new ReservationManager().GetAllReservation().ConvertToDtos();
        }
        #endregion

        #region update
        public AutoDto UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            try
            { 
                return new AutoManager().Update(auto.ConvertToEntity()).ConvertToDto();
            }
            catch (OptimisticConcurrencyException<Auto> ex)
            {
                throw new FaultException<AutoDto>(ex.MergedEntity.ConvertToDto(), ex.Message);
            }
}

        public KundeDto UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            try
            { 
                return new KundeManager().Update(kunde.ConvertToEntity()).ConvertToDto();

            }
            catch (OptimisticConcurrencyException<Kunde> ex)
            {
                throw new FaultException<KundeDto>(ex.MergedEntity.ConvertToDto(), ex.Message);
            }
}

        public ReservationDto UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                if (reservation.Bis - reservation.Von >= TimeSpan.FromDays(1))
                {
                    return new ReservationManager().Update(reservation.ConvertToEntity()).ConvertToDto();
                }
                throw new InvalidDateRangeException();
            }
            catch (OptimisticConcurrencyException<Reservation> ex)
            {
                throw new FaultException<ReservationDto>(ex.MergedEntity.ConvertToDto(), ex.Message);
            }
        }
#endregion
    }
}