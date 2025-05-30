using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories.repositories
{
    /// <summary>
    /// Repositorio que gestiona las operaciones CRUD para la entidad <see cref="ProduccionDetalle"/>.
    /// </summary>
    public class ProduccionDetalleRepository : IProduccionDetalle
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto <see cref="CounterTexDBContext"/>.</param>
        public ProduccionDetalleRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los registros de detalles de producción.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="ProduccionDetalle"/>.</returns>
        public async Task<IEnumerable<ProduccionDetalle>> GetAllAsync() => await _context.ProduccionDetalles.ToListAsync();

        /// <summary>
        /// Obtiene un detalle de producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador del detalle de producción.</param>
        /// <returns>Objeto <see cref="ProduccionDetalle"/> correspondiente o null si no se encuentra.</returns>
        public async Task<ProduccionDetalle> GetByIdAsync(int id) => await _context.ProduccionDetalles.FindAsync(id);

        /// <summary>
        /// Crea un nuevo detalle de producción en la base de datos.
        /// </summary>
        /// <param name="detalle">Objeto <see cref="ProduccionDetalle"/> a agregar.</param>
        /// <returns>El objeto creado con su ID asignado.</returns>
        public async Task<ProduccionDetalle> CreateAsync(ProduccionDetalle detalle)
        {
            _context.ProduccionDetalles.Add(detalle);
            await _context.SaveChangesAsync();
            return detalle;
        }

        /// <summary>
        /// Elimina un detalle de producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador del detalle de producción a eliminar.</param>
        public async Task DeleteAsync(int id)
        {
            var detalle = await _context.ProduccionDetalles.FindAsync(id);
            if (detalle != null)
            {
                _context.ProduccionDetalles.Remove(detalle);
                await _context.SaveChangesAsync();
            }
        }
    }
}
