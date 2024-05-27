using Microsoft.AspNetCore.Mvc;
using AMVTravels.Services;
using AMVTravels.Resources;
using AMVTravels.Services.Contract;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using AMVTravels.Models;
using Microsoft.AspNetCore.Authorization;

namespace AMVTravels.Controllers
{
    
    public class InicioController : Controller
    {
        private readonly IUserService _userService;
        public InicioController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario usuario)
        {
            usuario.Clave = Utils.EncriptarClave(usuario.Clave);

            Usuario usuarioCreado = await _userService.SaveUsuario(usuario);
            if (usuarioCreado != null)
            {
                return RedirectToAction("IniciarSesion", "Inicio");
            }
            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string nombreUsuario, string clave)
        {
            Usuario usuario = await _userService.GetUsuario(nombreUsuario, Utils.EncriptarClave(clave));
            if (usuario == null) {
                ViewData["Mensaje"] = "No se pudo encontrar el usuario";
                return View();
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Tours");
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "Inicio");
        }
    }
}
