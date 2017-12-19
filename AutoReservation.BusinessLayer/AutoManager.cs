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
    public class AutoManager
        : ManagerBase
    {
        // Example
        public List<Auto> GetAllAutos()
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                return dbContext.Autos.ToList();
            }
        }

        public Auto FindAutoById(int id)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                return dbContext.Autos
                    .Where(a => a.Id == id)
                    .FirstOrDefault();
            }
        }

        public Auto Insert(Auto auto)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                Auto insertedAuto = dbContext.Autos.Add(auto);
                dbContext.Entry(insertedAuto).State = EntityState.Added;
                dbContext.SaveChanges();
                return insertedAuto;
            }
        }

        public Auto Delete(Auto auto)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                dbContext.Entry(auto).State = EntityState.Deleted;
                dbContext.SaveChanges();
                return auto;
            }
        }

        public Auto Update(Auto auto)
        {
            using (KundenReservationContext dbContext = new KundenReservationContext())
            {
                try
                { 
                    dbContext.Entry(auto).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return auto;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw CreateOptimisticConcurrencyException<Auto>(dbContext, auto);
                }
        }
        }
    }
}