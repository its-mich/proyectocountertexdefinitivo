using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    public class HorarioRepository : IHorario
    {
        private readonly CounterTexDBContext _context;

        public HorarioRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Horario>> GetAllAsync() => await _context.Horarios.ToListAsync();

        public async Task<Horario> GetByIdAsync(int id) => await _context.Horarios.FindAsync(id);

        public async Task<Horario> CreateAsync(Horario horario)
        {
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();
            return horario;
        }

        public async Task DeleteAsync(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario != null)
            {
                _context.Horarios.Remove(horario);
                await _context.SaveChangesAsync();
            }
        }
    }

}
