using Microsoft.EntityFrameworkCore;
using AMVTravels.Models;
using AMVTravels.Services.Contract;

namespace AMVTravels.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AMVTravelContext _context;
        public UserService(AMVTravelContext context)
        {
            _context = context;
        }
        public async Task<Usuario> GetUsuario(string nombreUsuario, string clave)
        {
            Usuario usuarioEncontrado = await _context.Usuarios.Where(u=> u.NombreUsuario == nombreUsuario && u.Clave == clave).FirstOrDefaultAsync();
            return usuarioEncontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario usario)
        {
            _context.Usuarios.Add(usario);
            await _context.SaveChangesAsync();
            return usario;
        }
    }
}
