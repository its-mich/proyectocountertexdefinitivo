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

        public async Task<IEnumerable<MensajeChat>> GetAllAsync() => await _context.MensajesChat.ToListAsync();

        public async Task<MensajeChat> GetByIdAsync(int id) => await _context.MensajesChat.FindAsync(id);

        public async Task<MensajeChat> CreateAsync(MensajeChat mensaje)
        {
            _context.MensajesChat.Add(mensaje);
            await _context.SaveChangesAsync();
            return mensaje;
        }

        public async Task DeleteAsync(int id)
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
