using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para las operaciones CRUD sobre la entidad ProduccionDetalle.
    /// </summary>
    public interface IProduccionDetalle
    {
        /// <summary>
        /// Obtiene todos los detalles de producción.
        /// </summary>
        /// <returns>Una colección enumerable de objetos ProduccionDetalle.</returns>
        Task<IEnumerable<ProduccionDetalle>> GetAllAsync();

        /// <summary>
        /// Obtiene un detalle de producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador del detalle de producción.</param>
        /// <returns>El objeto ProduccionDetalle correspondiente al Id proporcionado.</returns>
        Task<ProduccionDetalle> GetByIdAsync(int id);

        /// <summary>
        /// Crea un nuevo detalle de producción.
        /// </summary>
        /// <param name="detalle">Objeto ProduccionDetalle a crear.</param>
        /// <returns>El detalle de producción creado.</returns>
        Task<ProduccionDetalle> CreateAsync(ProduccionDetalle detalle);

        /// <summary>
        /// Elimina un detalle de producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador del detalle de producción a eliminar.</param>
        Task DeleteAsync(int id);
    }
}
