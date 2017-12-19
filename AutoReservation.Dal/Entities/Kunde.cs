using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Runtime.Serialization;

namespace AutoReservation.Dal.Entities
{
    public class Kunde
    {
        [Timestamp] public byte[] RowVersion { get; set; }

        // Primary Key
        [Key] public int Id { get; set; }

        [Required] public DateTime Geburtsdatum { get; set; }
        [Required, StringLength(20)] public string Nachname { get; set; }
        [Required, StringLength(20)] public string Vorname { get; set; }

        // Navigation Property
        public ICollection<Reservation> Reservationen { get; set; }
    }
}
