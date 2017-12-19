using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;
using AutoReservation.BusinessLayer.Exceptions;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase
    {
        public List<Reservation> GetAllReservation()
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                return dbContext.Reservationen.ToList();
            }
        }

        public Reservation FindReservationByNr(int searchId)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                return dbContext.Reservationen
                    .SingleOrDefault(r => r.ReservationsNr == searchId);
            }
        }

        public Reservation Insert(Reservation res)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                Reservation insertedReservation = dbContext.Reservationen.Add(res);
                dbContext.Entry(insertedReservation).State = EntityState.Added;
                dbContext.SaveChanges();
                return insertedReservation;
            }
        }

        public Reservation Delete(Reservation res)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                dbContext.Entry(res).State = EntityState.Deleted;
                dbContext.SaveChanges();
                return res;
            }
        }

        public Reservation Update(Reservation res)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                try
                { 
                    dbContext.Entry(res).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return res;
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException<Reservation>(dbContext, res);
                }
            }
        }
    }
}