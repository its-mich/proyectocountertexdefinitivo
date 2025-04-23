using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Controllers;
using proyectocountertexdefinitivo.Models;

using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories.repositories
{
    public class ProduccionRepository : IProduccion
    {
        private readonly CounterTexDBContext _context;

        public ProduccionRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produccion>> GetAllAsync() => await _context.Producciones.ToListAsync();

        public async Task<Produccion> GetByIdAsync(int id) => await _context.Producciones.FindAsync(id);

        public async Task<Produccion> CreateAsync(Produccion produccion)
        {
            _context.Producciones.Add(produccion);
            await _context.SaveChangesAsync();
            return produccion;
        }   

        public async Task DeleteAsync(int id)
        {
            var produccion = await _context.Producciones.FindAsync(id);
            if (produccion != null)
            {
                _context.Producciones.Remove(produccion);
                await _context.SaveChangesAsync();
            }
        }
    }


}
