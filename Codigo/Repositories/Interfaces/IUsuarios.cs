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
        Task<List<Usuario>> GetUsuarios();

        /// <summary>
        /// Obtiene un usuario por su identificador único.
        /// </summary>
        /// <param name="id">El identificador del usuario.</param>
        /// <returns>El usuario correspondiente al id proporcionado, o null si no existe.</returns>
        Task<Usuario> GetUsuarioByIdAsync(int id);

        Task<Usuario?> GetUsuarioByCorreoAsync(string correo);

        Task<bool> GuardarTokenRecuperacionAsync(int usuarioId, string token);

        Task<Usuario?> GetUsuarioPorTokenAsync(string token);

        Task<bool> LimpiarTokenRecuperacionAsync(int usuarioId);

        Task<bool> EnviarCorreo(string destino, string codigo);

        /// <summary>
        /// Crea un nuevo usuario y lo almacena.
        /// </summary>
        /// <param name="usuario">El usuario a crear.</param>
        /// <returns>El usuario creado con sus datos actualizados.</returns>
        Task<bool> PostUsuarios(Usuario usuario);

        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        /// <param name="usuario">El usuario con los datos actualizados.</param>
        /// <returns>Una tarea asincrónica.</returns>
        Task<bool> PutUsuarios(Usuario usuario);

        /// <summary>
        /// Elimina un usuario según su identificador.
        /// </summary>
        /// <param name="id">El identificador del usuario a eliminar.</param>
        /// <returns>Una tarea asincrónica.</returns>
        Task<bool> DeleteUsuarios(int id);
    }
}
