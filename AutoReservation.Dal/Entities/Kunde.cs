using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public class Kunde
    {
        public byte[] RowVersion { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public int Id { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }

        // Navigation Property
        public ICollection<Reservation> Reservationen { get; set; }
    }
}
