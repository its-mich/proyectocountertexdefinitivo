using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    /// <summary>
    /// Repositorio para manejar operaciones CRUD relacionadas con la entidad Meta.
    /// </summary>
    public class MetaRepository : IMeta
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto de la base de datos.</param>
        public MetaRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las metas registradas.
        /// </summary>
        /// <returns>Lista de objetos <see cref="Meta"/>.</returns>
        public async Task<IEnumerable<Meta>> GetAllAsync() => await _context.Metas.ToListAsync();

        /// <summary>
        /// Obtiene una meta específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la meta.</param>
        /// <returns>Objeto <see cref="Meta"/> correspondiente o null si no se encuentra.</returns>
        public async Task<List<Meta>> GetByIdAsync(int usuarioId)
        {
            return await _context.Metas
                .Where(m => m.UsuarioId == usuarioId)
                .ToListAsync();
        }


        /// <summary>
        /// Crea una nueva meta en la base de datos.
        /// </summary>
        /// <param name="meta">Objeto <see cref="Meta"/> a registrar.</param>
        /// <returns>La meta creada.</returns>
        public async Task<Meta> CreateAsync(Meta meta)
        {
            _context.Metas.Add(meta);
            await _context.SaveChangesAsync();
            return meta;
        }

        /// <summary>
        /// Elimina una meta existente por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la meta a eliminar.</param>
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
