using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de entidades <see cref="Usuario"/> en la base de datos.
    /// Implementa operaciones CRUD usando Entity Framework.
    /// </summary>
    public class UsuarioRepository : IUsuarios
    {
        /// <summary>
        /// Contexto de base de datos utilizado para acceder a las entidades.
        /// </summary>
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia de <see cref="CounterTexDBContext"/>.</param>
        public UsuarioRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados en la base de datos.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="Usuario"/>.</returns>
        public async Task<IEnumerable<Usuario>> GetAllAsync() => await _context.Usuarios.ToListAsync();

        /// <summary>
        /// Obtiene un usuario por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del usuario.</param>
        /// <returns>El objeto <see cref="Usuario"/> si se encuentra; de lo contrario, null.</returns>
        public async Task<Usuario> GetByIdAsync(int id) => await _context.Usuarios.FindAsync(id);

        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Instancia de <see cref="Usuario"/> a agregar.</param>
        /// <returns>El objeto <see cref="Usuario"/> creado.</returns>
        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="usuario">Instancia de <see cref="Usuario"/> con los nuevos datos.</param>
        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina un usuario de la base de datos por su identificador.
        /// </summary>
        /// <param name="id">Identificador del usuario a eliminar.</param>
        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
