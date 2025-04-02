using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    public class OperacionEmpleadoRepository : IOperacionEmpleadoRepository
    {
        private readonly CountertexDbContext _context;
        private Operacion OperacionesEmpleados;

        public OperacionEmpleadoRepository(CountertexDbContext context)
        {
            _context = context;
        }

        public async Task<List<OperacionesEmpleado>> GetAllAsync()
        {
            return await _context.OperacionEmpleados.ToListAsync(); ;
        }

        public async Task<OperacionesEmpleado> GetByIdAsync(int id)
        {
            return await _context.OperacionEmpleados.FindAsync(id);
        }

        public async Task<bool> AddAsync(OperacionesEmpleado operacionEmpleado)
        {

            await _context.OperacionEmpleados.AddAsync(operacionEmpleado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(OperacionesEmpleado operacionEmpleado)
        {
            _context.Operaciones.Update(OperacionesEmpleados);
            await _context.SaveChangesAsync();
            return true; // Asegúrate de que devuelva un bool si la interfaz lo requiere
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Operaciones.FindAsync(id);

            if (entity == null)
                return false;
            _context.Operaciones.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
