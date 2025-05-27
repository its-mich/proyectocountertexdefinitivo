using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Controllers;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories.repositories
{
    /// <summary>
    /// Repositorio que gestiona las operaciones CRUD para la entidad <see cref="Produccion"/>.
    /// </summary>
    public class ProduccionRepository : IProduccion
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto <see cref="CounterTexDBContext"/>.</param>
        public ProduccionRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las producciones registradas.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="Produccion"/>.</returns>
        public async Task<IEnumerable<Produccion>> GetAllAsync() => await _context.Producciones.ToListAsync();

        /// <summary>
        /// Obtiene una producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la producción.</param>
        /// <returns>El objeto <see cref="Produccion"/> correspondiente o null si no existe.</returns>
        public async Task<Produccion> GetByIdAsync(int id) => await _context.Producciones.FindAsync(id);

        /// <summary>
        /// Crea una nueva producción en la base de datos.
        /// </summary>
        /// <param name="produccion">Objeto <see cref="Produccion"/> a registrar.</param>
        /// <returns>L
