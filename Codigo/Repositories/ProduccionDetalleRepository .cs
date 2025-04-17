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

        public async Task<IEnumerable<Registros>> GetAllAsync() => await _context.ProduccionDetalle.ToListAsync();

        public async Task<Registros> GetByIdAsync(int id) => await _context.ProduccionDetalle.FindAsync(id);

        public async Task<Registros> CreateAsync(Registros detalle)
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
