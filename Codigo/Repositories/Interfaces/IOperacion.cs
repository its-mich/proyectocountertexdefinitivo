using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para gestionar operaciones CRUD sobre la entidad Operacion.
    /// </summary>
    public interface IOperacion
    {
        /// <summary>
        /// Obtiene todas las operaciones.
        /// </summary>
        /// <returns>Una colección enumerable de objetos Operacion.</returns>
        Task<IEnumerable<Operacion>> GetAllAsync();

        /// <summary>
        /// Obtiene una operación por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la operación.</param>
        /// <returns>El objeto Operacion correspondiente al Id proporcionado.</returns>
        Task<Operacion> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva operación.
        /// </summary>
        /// <param name="operacion">Objeto Operacion a crear.</param>
        /// <returns>La operación creada.</returns>
        Task<Operacion> CreateAsync(Operacion operacion);

        /// <summary>
        /// Actualiza una operación existente.
        /// </summary>
        /// <param name="operacion">Objeto Operacion con los datos actualizados.</param>
        Task UpdateAsync(Operacion operacion);

        /// <summary>
        /// Elimina una operación por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la operación a eliminar.</param>
        Task DeleteAsync(int id);
    }
}
