using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para operaciones CRUD relacionadas con la entidad Horario.
    /// </summary>
    public interface IHorario
    {
        /// <summary>
        /// Obtiene todos los horarios almacenados.
        /// </summary>
        /// <returns>Una colección enumerable de objetos Horario.</returns>
        Task<IEnumerable<Horario>> GetAllAsync();

        /// <summary>
        /// Obtiene un horario específico por su Id.
        /// </summary>
        /// <param name="id">Identificador del horario.</param>
        /// <returns>El objeto Horario correspondiente al Id proporcionado.</returns>
        Task<Horario> GetByIdAsync(int id);

        /// <summary>
        /// Crea un nuevo horario.
        /// </summary>
        /// <param name="horario">Objeto Horario a crear.</param>
        /// <returns>El objeto Horario creado con sus datos actualizados.</returns>
        Task<Horario> CreateAsync(Horario horario);

        /// <summary>
        /// Elimina un horario por su Id.
        /// </summary>
        /// <param name="id">Identificador del horario a eliminar.</param>
        Task DeleteAsync(int id);
    }
}
