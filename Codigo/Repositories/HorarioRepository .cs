using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    /// <summary>
    /// Repositorio que gestiona las operaciones CRUD para la entidad <see cref="Horario"/>.
    /// </summary>
    public class HorarioRepository : IHorario
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto <see cref="CounterTexDBContext"/>.</param>
        public HorarioRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los registros de horarios.
        /// </summary>
        /// <returns>Una colección de objetos <see cref="Horario"/>.</returns>
        public async Task<IEnumerable<Horario>> GetAllAsync() => await _context.Horarios.ToListAsync();


        public async Task<IEnumerable<Horario>> GetByFechaAsync(DateTime fecha)
        {
            return await _context.Horarios
                .Where(h => h.Fecha.Date == fecha.Date)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un horario específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del horario.</param>
        /// <returns>El objeto <see cref="Horario"/> encontrado o null si no existe.</returns>
        public async Task<List<Horario>> GetByIdAsync(int Id)
        {
            return await _context.Horarios
                .Where(m => m.HorarioId == Id)
                .ToListAsync();
        }


        /// <summary>
        /// Crea un nuevo horario en la base de datos.
        /// </summary>
        /// <param name="horario">Objeto <see cref="Horario"/> a crear.</param>
        /// <returns>El objeto <see cref="Horario"/> creado.</returns>
        public async Task<Horario> CreateAsync(Horario horario)
        {
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();
            return horario;
        }

        /// <summary>
        /// Elimina un horario por su identificador.
        /// </summary>
        /// <param name="id">Identificador del horario a eliminar.</param>
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
