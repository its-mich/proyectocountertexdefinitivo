using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para las operaciones CRUD sobre la entidad Produccion.
    /// </summary>
    public interface IProduccion
    {
        /// <summary>
        /// Obtiene todas las producciones.
        /// </summary>
        /// <returns>Una colección enumerable de objetos Produccion.</returns>
        Task<IEnumerable<Produccion>> GetAllAsync();

        /// <summary>
        /// Obtiene una producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la producción.</param>
        /// <returns>El objeto Produccion correspondiente al Id proporcionado.</returns>
        Task<Produccion> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva producción.
        /// </summary>
        /// <param name="produccion">Objeto Produccion a crear.</param>
        /// <returns>La producción creada.</returns>
        Task<Produccion> CreateAsync(Produccion produccion);

        /// <summary>
        /// Elimina una producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la producción a eliminar.</param>
        Task DeleteAsync(int id);
    }
}
