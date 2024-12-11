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
        public async Task<List<Registro>> GetRegistro()
        {
            var data = await _context.Registros.ToListAsync();
            return data;
        }
        public async Task<bool> PostRegistro(Registro registro)
        {
            await _context.Registros.AddAsync(registro);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PutRegistro(Registro registro)
        {
            _context.Registros.Update(registro);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteRegistro(Registro registro)
        {
            _context.Registros.Remove(registro);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteRegistro(int id)
        {
            throw new NotImplementedException();
        }
    }
}
