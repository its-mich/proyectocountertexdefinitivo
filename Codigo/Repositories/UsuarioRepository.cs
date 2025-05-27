using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    public class UsuarioRepository : IUsuarios
    {
        private readonly CounterTexDBContext _context;

        public UsuarioRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync() => await _context.Usuarios.ToListAsync();

        public async Task<Usuario> GetByIdAsync(int id) => await _context.Usuarios.FindAsync(id);

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }

}
