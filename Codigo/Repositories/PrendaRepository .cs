using proyectocountertexdefinitivo.Models;

using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using proyectocountertexdefinitivo.contexto;

namespace proyectocountertexdefinitivo.Repositories
{
    public class PrendaRepository : IPrenda
    {
        private readonly CounterTexDBContext _context;

        public PrendaRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prenda>> GetAllAsync() => await _context.Prendas.ToListAsync();

        public async Task<Prenda> GetByIdAsync(int id) => await _context.Prendas.FindAsync(id);

        public async Task<Prenda> CreateAsync(Prenda prenda)
        {
            _context.Prendas.Add(prenda);
            await _context.SaveChangesAsync();
            return prenda;
        }

        public async Task UpdateAsync(Prenda prenda)
        {
            _context.Entry(prenda).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var prenda = await _context.Prendas.FindAsync(id);
            if (prenda != null)
            {
                _context.Prendas.Remove(prenda);
                await _context.SaveChangesAsync();
            }
        }
    }

}
