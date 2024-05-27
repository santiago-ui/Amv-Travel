using System;
using System.Collections.Generic;

namespace AMVTravels.Models
{
    public partial class Tour
    {
        public Tour()
        {
            Reservas = new HashSet<Reserva>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Destino { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? Precio { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
