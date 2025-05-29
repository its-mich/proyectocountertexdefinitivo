using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para operaciones CRUD sobre la entidad Prenda.
    /// </summary>
    public interface IPrenda
    {
        /// <summary>
        /// Obtiene todas las prendas.
        /// </summary>
        /// <returns>Una colección enumerable de objetos Prenda.</returns>
        Task<IEnumerable<Prenda>> GetAllAsync();

        /// <summary>
        /// Obtiene una prenda por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la prenda.</param>
        /// <returns>El objeto Prenda correspondiente al Id proporcionado.</returns>
        Task<Prenda> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva prenda.
        /// </summary>
        /// <param name="prenda">Objeto Prenda a crear.</param>
        /// <returns>La prenda creada.</returns>
        Task<Prenda> CreateAsync(Prenda prenda);

        /// <summary>
        /// Actualiza una prenda existente.
        /// </summary>
        /// <param name="prenda">Objeto Prenda con los datos actualizados.</param>
        Task UpdateAsync(Prenda prenda);

        /// <summary>
        /// Elimina una prenda por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la prenda a eliminar.</param>
        Task DeleteAsync(int id);
    }
}
