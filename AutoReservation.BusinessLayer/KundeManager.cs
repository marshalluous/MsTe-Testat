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
    public class KundeManager
        : ManagerBase
    {
        public List<Kunde> GetAllKunde()
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                return dbContext.Kunden.ToList();
            }
        }

        public Kunde FindKundeById(int id)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                return dbContext.Kunden
                    .Where(k => k.Id == id)
                    .FirstOrDefault();
            }
        }

        public Kunde Insert(Kunde kunde)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                Kunde insertedKunde = dbContext.Kunden.Add(kunde);
                dbContext.Entry(insertedKunde).State = EntityState.Added;
                dbContext.SaveChanges();
                return insertedKunde;
            }
        }

        public Kunde Delete(Kunde kunde)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                dbContext.Entry(kunde).State = EntityState.Deleted;
                dbContext.SaveChanges();
                return kunde;
            }
        }

        public Kunde Update(Kunde kunde)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                try
                {
                    dbContext.Entry(kunde).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return kunde;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ManagerBase.CreateOptimisticConcurrencyException<Kunde>(dbContext, kunde);
                }
            }
        }
    }
}