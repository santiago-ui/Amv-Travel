using Microsoft.EntityFrameworkCore;
using AMVTravels.Models;

namespace AMVTravels.Services.Contract
{
    public interface IUserService
    {
        Task<Usuario> GetUsuario(string correo, string clave);

        Task<Usuario> SaveUsuario(Usuario usario);
    }
}
