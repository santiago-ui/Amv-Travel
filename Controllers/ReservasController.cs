using AMVTravels.Models;
using AMVTravels.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AMVTravels.Controllers
{
    [Authorize]
    public class ReservasController : Controller
    {
        private readonly GestorReservas _gestorReservas;

        public ReservasController(GestorReservas gestorReservas)
        {
            _gestorReservas = gestorReservas;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult VerReservas()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ObtenerReservas()
        {
            var reservas = _gestorReservas.ObtenerReservas();
            var reservasConTour = reservas.Select(r => new
            {
                r.Id,
                r.Cliente,
                r.FechaReserva,
                r.TourId,
                Tour = _gestorReservas.MostrarTours().FirstOrDefault(t => t.Id == r.TourId)?.Nombre
            }).ToList();
            return Json(reservasConTour);
        }

        [HttpGet]
        public IActionResult ReservarTour()
        {
            var tours = _gestorReservas.MostrarTours();
            ViewBag.Tours = new SelectList(tours, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public IActionResult ReservarTour(Reserva reserva) 
        {
            if (ModelState.IsValid) 
            { 
                _gestorReservas.ReservarTour(reserva);
                return Json(new { success = true, message = "Reserva realizada exitosamente." });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new {success = false, errors});
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            var reserva = _gestorReservas.EliminarReserva(id);
            if (reserva)
            {
                return Ok();
            }
            else {
                return NotFound();
            }
            
        }
    }
}
