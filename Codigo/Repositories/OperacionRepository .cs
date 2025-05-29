using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories.repositories
{
    /// <summary>
    /// Repositorio que gestiona las operaciones CRUD para la entidad <see cref="Operacion"/>.
    /// </summary>
    public class OperacionRepository : IOperacion
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto <see cref="CounterTexDBContext"/>.</param>
        public OperacionRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las operaciones registradas.
        /// </summary>
        /// <returns>Una colección de objetos <see cref="Operacion"/>.</returns>
        public async Task<IEnumerable<Operacion>> GetAllAsync() => await _context.Operaciones.ToListAsync();

        /// <summary>
        /// Obtiene una operación específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la operación.</param>
        /// <returns>La operación encontrada o null si no existe.</returns>
        public async Task<Operacion> GetByIdAsync(int id) => await _context.Operaciones.FindAsync(id);

        /// <summary>
        /// Crea una nueva operación en la base de datos.
        /// </summary>
        /// <param name="operacion">Objeto <see cref="Operacion"/> a crear.</param>
        /// <returns>La operación creada.</returns>
        public async Task<Operacion> CreateAsync(Operacion operacion)
        {
            _context.Operaciones.Add(operacion);
            await _context.SaveChangesAsync();
            return operacion;
        }

        /// <summary>
        /// Actualiza una operación existente en la base de datos.
        /// </summary>
        /// <param name="operacion">Objeto <see cref="Operacion"/> con los datos actualizados.</param>
        public async Task UpdateAsync(Operacion operacion)
        {
            _context.Entry(operacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina una operación por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la operación a eliminar.</param>
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
