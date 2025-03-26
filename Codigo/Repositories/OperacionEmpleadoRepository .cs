using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;

namespace proyectocountertexdefinitivo.Repositories
{

    
        public class OperacionEmpleadoRepository : IOperacionEmpleadoRepository
        {
            private readonly CountertexDbContext _context;

            public OperacionEmpleadoRepository(CountertexDbContext context)
            {
                _context = context;
            }

            public async Task<List<OperacionEmpleado>> GetAllAsync()
            {
                return await _context.OperacionEmpleado.Include(o => o.Operacion).Include(e => e.PerfilEmpleado).ToListAsync();
            }

            public async Task<OperacionEmpleado> GetByIdAsync(int id)
            {
                return await _context.OperacionEmpleado.FindAsync(id);
            }

            public async Task<bool> AddAsync(OperacionEmpleado operacionEmpleado)
            {
                await _context.OperacionEmpleado.AddAsync(operacionEmpleado);
                return await _context.SaveAsync();
            }

            public async Task<bool> UpdateAsync(OperacionEmpleado operacionEmpleado)
            {
                _context.OperacionEmpleado.Update(operacionEmpleado);
                return await _context.SaveAsync();
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var entity = await _context.OperacionEmpleado.FindAsync(id);
                if (entity == null) return false;
                _context.OperacionEmpleado.Remove(entity);
                return await _context.SaveAsync();
            }
        }
    
}
