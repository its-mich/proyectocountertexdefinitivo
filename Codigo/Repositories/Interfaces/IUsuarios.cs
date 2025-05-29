using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Define los métodos para el acceso y manipulación de datos de usuarios.
    /// </summary>
    public interface IUsuarios
    {
        /// <summary>
        /// Obtiene todos los usuarios almacenados.
        /// </summary>
        /// <returns>Una colección de usuarios.</returns>
        Task<IEnumerable<Usuario>> GetAllAsync();

        /// <summary>
        /// Obtiene un usuario por su identificador único.
        /// </summary>
        /// <param name="id">El identificador del usuario.</param>
        /// <returns>El usuario correspondiente al id proporcionado, o null si no existe.</returns>
        Task<Usuario> GetByIdAsync(int id);

        /// <summary>
        /// Crea un nuevo usuario y lo almacena.
        /// </summary>
        /// <param name="usuario">El usuario a crear.</param>
        /// <returns>El usuario creado con sus datos actualizados.</returns>
        Task<Usuario> CreateAsync(Usuario usuario);

        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        /// <param name="usuario">El usuario con los datos actualizados.</param>
        /// <returns>Una tarea asincrónica.</returns>
        Task UpdateAsync(Usuario usuario);

        /// <summary>
        /// Elimina un usuario según su identificador.
        /// </summary>
        /// <param name="id">El identificador del usuario a eliminar.</param>
        /// <returns>Una tarea asincrónica.</returns>
        Task DeleteAsync(int id);
    }
}
