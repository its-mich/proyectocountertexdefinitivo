using proyectocountertexdefinitivo.Controllers;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para operaciones CRUD relacionadas con la entidad Contacto.
    /// </summary>
    public interface IContacto
    {
        /// <summary>
        /// Obtiene todos los contactos de la base de datos.
        /// </summary>
        /// <returns>Una colección enumerable de objetos Contacto.</returns>
        Task<IEnumerable<Contacto>> GetAllAsync();

        /// <summary>
        /// Obtiene un contacto específico por su Id.
        /// </summary>
        /// <param name="id">Identificador del contacto.</param>
        /// <returns>El objeto Contacto correspondiente al Id proporcionado.</returns>
        Task<Contacto> GetByIdAsync(int id);

        /// <summary>
        /// Crea un nuevo contacto en la base de datos.
        /// </summary>
        /// <param name="contacto">Objeto Contacto a crear.</param>
        /// <returns>El objeto Contacto creado con sus datos actualizados.</returns>
        Task<Contacto> CreateAsync(Contacto contacto);

        /// <summary>
        /// Elimina un contacto por su Id.
        /// </summary>
        /// <param name="id">Identificador del contacto a eliminar.</param>
        Task DeleteAsync(int id);
    }
}
