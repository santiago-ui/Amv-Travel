using Microsoft.AspNetCore.Mvc;
using AMVTravels.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AMVTravels.Services
{
    public class GestorReservas
    {
        private readonly AMVTravelContext _context;
        public List<Tour> Tours { get; set; }
        public List<Reserva> Reservas { get; set; }

        public GestorReservas(AMVTravelContext context)
        {
            _context = context;
            Tours = new List<Tour>();
            Reservas = new List<Reserva>();
        }

        public List<Tour> MostrarTours()
        {
            Tours = (from tour in _context.Tours
                     select tour).ToList(); 
            return Tours;
        }

        public void AgregarTour(Tour tour)
        {
            Tours.Add(tour);
            _context.Tours.Add(tour);
            _context.SaveChanges();
        }

        public void ReservarTour(Reserva reserva)
        {
            Reservas.Add(reserva);
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
        }

        public List<Reserva> ObtenerReservas()
        {
            Reservas = (from reserva in _context.Reservas
                        select reserva).Include(r => r.Tour).ToList();
            return Reservas;
        }

        public bool EliminarReserva(int id)
        {
            var reserva = _context.Reservas.FirstOrDefault(r => r.Id == id);
            if (reserva == null)
            {
                return false;
            }

            _context.Reservas.Remove(reserva);
            _context.SaveChanges();

            return true;
        }
    }
}
