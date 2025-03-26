using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    public class OperacionRepository
    {
        public class OperacionRepository : IOperacionRepository
        {
            private readonly DatabaseService _context;

            public OperacionRepository(DatabaseService context)
            {
                _context = context;
            }

            public async Task<List<Operacion>> GetAllAsync()
            {
                return await _context.Operacion.ToListAsync();
            }

            public async Task<Operacion> GetByIdAsync(int id)
            {
                return await _context.Operacion.FindAsync(id);
            }

            public async Task<bool> AddAsync(Operacion operacion)
            {
                await _context.Operacion.AddAsync(operacion);
                return await _context.SaveAsync();
            }

            public async Task<bool> UpdateAsync(Operacion operacion)
            {
                _context.Operacion.Update(operacion);
                return await _context.SaveAsync();
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var entity = await _context.Operacion.FindAsync(id);
                if (entity == null) return false;
                _context.Operacion.Remove(entity);
                return await _context.SaveAsync();
            }
        }
    }
}
