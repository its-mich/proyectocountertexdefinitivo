using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    public class MetaRepository : IMeta
    {
        private readonly CounterTexDBContext _context;

        public MetaRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Meta>> GetAllAsync() => await _context.Metas.ToListAsync();

        public async Task<Meta> GetByIdAsync(int id) => await _context.Metas.FindAsync(id);

        public async Task<Meta> CreateAsync(Meta meta)
        {
            _context.Metas.Add(meta);
            await _context.SaveChangesAsync();
            return meta;
        }

        public async Task DeleteAsync(int id)
        {
            var meta = await _context.Metas.FindAsync(id);
            if (meta != null)
            {
                _context.Metas.Remove(meta);
                await _context.SaveChangesAsync();
            }
        }
    }

}
