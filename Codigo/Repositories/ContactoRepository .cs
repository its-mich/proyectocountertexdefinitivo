using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    /// <summary>
    /// Implementación del repositorio para la entidad <see cref="Contacto"/>.
    /// Proporciona métodos para operaciones CRUD asincrónicas sobre contactos.
    /// </summary>
    public class ContactoRepository : IContacto
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ContactoRepository"/> con el contexto de base de datos proporcionado.
        /// </summary>
        /// <param name="context">Contexto de base de datos para acceder a la tabla Contacto.</param>
        public ContactoRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los contactos registrados en la base de datos.
        /// </summary>
        /// <returns>Una colección de objetos <see cref="Contacto"/>.</returns>
        public async Task<IEnumerable<Contacto>> GetAllAsync() => await _context.Contacto.ToListAsync();

        /// <summary>
        /// Obtiene un contacto por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del contacto a buscar.</param>
        /// <returns>El contacto encontrado o null si no existe.</returns>
        public async Task<Contacto> GetByIdAsync(int id) => await _context.Contacto.FindAsync(id);

        /// <summary>
        /// Crea un nuevo contacto en la base de datos.
        /// </summary>
        /// <param name="contacto">Objeto <see cref="Contacto"/> con los datos a guardar.</param>
        /// <returns>El contacto creado con su Id asignado.</returns>
        public async Task<Contacto> CreateAsync(Contacto contacto)
        {
            _context.Contacto.Add(contacto);
            await _context.SaveChangesAsync();
            return contacto;
        }

        /// <summary>
        /// Elimina un contacto de la base de datos por su Id.
        /// </summary>
        /// <param name="id">Identificador del contacto a eliminar.</param>
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
