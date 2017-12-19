using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AutoReservation.Dal.Entities
{
    [Table("Reservation")]
    public class Reservation
    {
        [Timestamp] public byte[] RowVersion { get; set; }
        [Key] public int ReservationsNr { get; set; }

        [Required] public DateTime Von { get; set; }
        [Required] public DateTime Bis { get; set; }

        // Foreign Keys
        [Required] public int AutoId { get; set; }
        [Required] public int KundeId { get; set; }

        // Navigation Property
        [ForeignKey("AutoId")] public Auto Auto { get; set; }
        [ForeignKey("KundeId")] public Kunde Kunde { get; set; }
    }
}
