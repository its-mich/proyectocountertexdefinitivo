using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    public class MensajesChatRepository : IMensajesChat
    {
        private readonly CounterTexDBContext _context;

        public MensajesChatRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MensajeChat>> ObtenerTodosAsync()
        {
            return await _context.MensajesChat.ToListAsync();
        }

        public async Task<MensajeChat> ObtenerPorIdAsync(int id)
        {
            return await _context.MensajesChat.FindAsync(id);
        }

        public async Task<IEnumerable<MensajeChat>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            return await _context.MensajesChat
                .Where(m => m.RemitenteId == usuarioId)
                .ToListAsync();
        }

        public async Task AgregarAsync(MensajeChat mensaje)
        {
            _context.MensajesChat.Add(mensaje);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(MensajeChat mensaje)
        {
            _context.MensajesChat.Update(mensaje);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var mensaje = await _context.MensajesChat.FindAsync(id);
            if (mensaje != null)
            {
                _context.MensajesChat.Remove(mensaje);
                await _context.SaveChangesAsync();
            }
        }
    }
}
