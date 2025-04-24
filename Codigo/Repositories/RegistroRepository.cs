using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    public class RegistroRepository : IRegistro
    {
        private readonly CounterTexDBContext _context;
        public RegistroRepository(CounterTexDBContext context)
        {
            this._context = context;
        }
        public async Task<List<Registros>> GetRegistro()
        {
            var data = await _context.ProduccionDetalle.ToListAsync();
            return data;
        }
        public async Task<bool> PostRegistro(Registros registro)
        {
            await _context.ProduccionDetalle.AddAsync(registro);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PutRegistro(Registros registro)
        {
            _context.ProduccionDetalle.Update(registro);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteRegistro(int id)
        {
            var registro = await _context.ProduccionDetalle.FindAsync(id); // Usar 'context' en lugar de '_context'
            if (registro == null) return false; // Si no existe, devolver 'false'

            _context.ProduccionDetalle.Remove(registro); // Usar 'context'
            await _context.SaveChangesAsync(); // Corregir 'SaveAsync' por 'SaveChangesAsync'
            return true;
        }


    }
}
