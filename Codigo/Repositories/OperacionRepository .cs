using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;


namespace proyectocountertexdefinitivo.Repositories.repositories
{
    public class OperacionRepository : IOperacion
    {
        private readonly CounterTexDBContext _context;

        public OperacionRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Operacion>> GetAllAsync() => await _context.Operaciones.ToListAsync();

        public async Task<Operacion> GetByIdAsync(int id) => await _context.Operaciones.FindAsync(id);

        public async Task<Operacion> CreateAsync(Operacion operacion)
        {
            _context.Operaciones.Add(operacion);
            await _context.SaveChangesAsync();
            return operacion;
        }

        public async Task UpdateAsync(Operacion operacion)
        {
            _context.Entry(operacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var operacion = await _context.Operaciones.FindAsync(id);
            if (operacion != null)
            {
                _context.Operaciones.Remove(operacion);
                await _context.SaveChangesAsync();
            }
        }
    }

}
