using AMVTravels.Models;
using AMVTravels.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AMVTravels.Controllers
{
    [Authorize]
    public class ToursController : Controller
    {
        private readonly GestorReservas _gestorReservas;
        public ToursController(GestorReservas gestorReservas)
        {
            _gestorReservas = gestorReservas;
        }
        public IActionResult Index()
        {
            var tours = _gestorReservas.MostrarTours();
            return View(tours);
        }

        [HttpGet]
        public IActionResult ObtenerTours()
        {
            var tours = _gestorReservas.MostrarTours();
            return Json(tours);
        }

        [HttpGet]
        public IActionResult AgregarTour()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarTour(Tour tour)
        {
            if (ModelState.IsValid)
            {
                _gestorReservas.AgregarTour(tour);
                return Json(new { success = true, message = "Tour agregado exitosamente." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors });
        }
        [HttpGet]
        public IActionResult Detalles(int id)
        {
            var tour = _gestorReservas.MostrarTours().FirstOrDefault(t => t.Id == id);
            if (tour == null)
            {
                return NotFound();
            }
            return Json(tour);
        }
        
    }
}
