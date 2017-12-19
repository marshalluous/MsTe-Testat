using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AutoReservation.Dal.Entities
{
    public abstract class Auto
    {
        [Timestamp] public byte[] RowVersion { get; set; }

        // Primary Key
        [Key] public int Id { get; set; }

        [Required] public string Marke { get; set; }
        [Required] public int Tagestarif { get; set; }

        // Navigation Property
        public ICollection<Reservation> Reservationen { get; set; }
    }

    public class StandardAuto : Auto
    {

    }

    public class LuxusklasseAuto : Auto
    {
        public int Basistarif { get; set; }
    }

    public class MittelklasseAuto : Auto
    {

    }

}
