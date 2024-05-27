using System;
using System.Collections.Generic;

namespace AMVTravels.Models
{
    public partial class Reserva
    {
        public int Id { get; set; }
        public string? Cliente { get; set; }
        public DateTime? FechaReserva { get; set; }
        public int? TourId { get; set; }

        public virtual Tour? Tour { get; set; }
    }
}
