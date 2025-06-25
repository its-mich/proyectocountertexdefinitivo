using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para el manejo de operaciones CRUD sobre la entidad Meta.
    /// </summary>
    public interface IMeta
    {
        /// <summary>
        /// Obtiene todas las metas.
        /// </summary>
        /// <returns>Una colección enumerable de objetos Meta.</returns>
        Task<IEnumerable<Meta>> GetAllAsync();

        /// <summary>
        /// Obtiene una meta por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la meta.</param>
        /// <returns>El objeto Meta correspondiente al Id proporcionado.</returns>
        Task <List<Meta>> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva meta.
        /// </summary>
        /// <param name="meta">Objeto Meta a crear.</param>
        /// <returns>La meta creada.</returns>
        Task<Meta> CreateAsync(Meta meta);

        /// <summary>
        /// Elimina una meta por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la meta a eliminar.</param>
        Task DeleteAsync(int id);
    }
}
