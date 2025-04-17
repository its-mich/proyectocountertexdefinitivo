using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Controllers;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    public class ContactoRepository : IContacto
    {
        private readonly CounterTexDBContext _context;

        public ContactoRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contacto>> GetAllAsync() => await _context.Contacto.ToListAsync();

        public async Task<Contacto> GetByIdAsync(int id) => await _context.Contacto.FindAsync(id);

        public async Task<Contacto> CreateAsync(Contacto contacto)
        {
            _context.Contacto.Add(contacto);
            await _context.SaveChangesAsync();
            return contacto;
        }

        public async Task DeleteAsync(int id)
        {
            var contacto = await _context.Contacto.FindAsync(id);
            if (contacto != null)
            {
                _context.Contacto.Remove(contacto);
                await _context.SaveChangesAsync();
            }
        }
    }

}
