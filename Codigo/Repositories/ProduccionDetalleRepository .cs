using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories.repositories
{
    public class ProduccionDetalleRepository : IProduccionDetalle
    {
        private readonly CounterTexDBContext _context;

        public ProduccionDetalleRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProduccionDetalle>> GetAllAsync() => await _context.ProduccionDetalle.ToListAsync();

        public async Task<ProduccionDetalle> GetByIdAsync(int id) => await _context.ProduccionDetalle.FindAsync(id);

        public async Task<ProduccionDetalle> CreateAsync(ProduccionDetalle detalle)
        {
            _context.ProduccionDetalle.Add(detalle);
            await _context.SaveChangesAsync();
            return detalle;
        }

        public async Task DeleteAsync(int id)
        {
            var detalle = await _context.ProduccionDetalle.FindAsync(id);
            if (detalle != null)
            {
                _context.ProduccionDetalle.Remove(detalle);
                await _context.SaveChangesAsync();
            }
        }
    }

}
