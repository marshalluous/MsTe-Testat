using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        public byte[] RowVersion { get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        public int ReservationsNr { get; set; }

        // Foreign Keys
        public int AutoId { get; set; }
        public int KundeId { get; set; }

        // Navigation Property
        public Auto Auto { get; set; }
        public Kunde Kunde { get; set; }
    }
}
