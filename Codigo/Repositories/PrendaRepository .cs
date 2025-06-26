using proyectocountertexdefinitivo.Models;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using proyectocountertexdefinitivo.contexto;

namespace proyectocountertexdefinitivo.Repositories
{
    /// <summary>
    /// Repositorio que gestiona las operaciones CRUD para la entidad <see cref="Prenda"/>.
    /// </summary>
    public class PrendaRepository : IPrenda
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto <see cref="CounterTexDBContext"/>.</param>
        public PrendaRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las prendas almacenadas.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="Prenda"/>.</returns>
        public async Task<IEnumerable<Prenda>> GetAllAsync() => await _context.Prendas.ToListAsync();

        /// <summary>
        /// Obtiene una prenda por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la prenda.</param>
        /// <returns>El objeto <see cref="Prenda"/> correspondiente o null si no se encuentra.</returns>
        public async Task<Prenda> GetByIdAsync(int id) => await _context.Prendas.FindAsync(id);

        /// <summary>
        /// Crea una nueva prenda en la base de datos.
        /// </summary>
        /// <param name="prenda">Objeto <see cref="Prenda"/> a agregar.</param>
        /// <returns>La prenda creada con su ID asignado.</returns>
        public async Task<Prenda> CreateAsync(Prenda prenda)
        {
            _context.Prendas.Add(prenda);
            await _context.SaveChangesAsync();
            return prenda;
        }

        /// <summary>
        /// Actualiza una prenda existente.
        /// </summary>
        /// <param name="prenda">Objeto <see cref="Prenda"/> con los datos actualizados.</param>
        public async Task UpdateAsync(Prenda prenda)
        {
            var existente = await _context.Prendas.FindAsync(prenda.Id);
            if (existente != null)
            {
                existente.Nombre = prenda.Nombre;
                existente.Genero = prenda.Genero;
                existente.Color = prenda.Color;
                existente.CantidadPrendas = prenda.CantidadPrendas;

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Elimina una prenda por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la prenda a eliminar.</param>
        public async Task DeleteAsync(int id)
        {
            var prenda = await _context.Prendas.FindAsync(id);
            if (prenda != null)
            {
                _context.Prendas.Remove(prenda);
                await _context.SaveChangesAsync();
            }
        }

        // En ProduccionRepository.cs
        public async Task<IEnumerable<Produccion>> GetProduccionesPorUsuarioIdAsync(int usuarioId)
        {
            return await _context.Producciones
                .Where(p => p.UsuarioId == usuarioId)
                .Include(p => p.Prenda)
                .Include(p => p.ProduccionDetalles)
                    .ThenInclude(d => d.Operacion)
                .ToListAsync();
        }

    }
}
