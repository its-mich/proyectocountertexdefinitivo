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
        /// <returns>Una lista de usuarios.</returns>
        Task<List<Usuario>> GetUsuarios();

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>Usuario si existe, o null.</returns>
        Task<Usuario?> GetUsuarioByIdAsync(int id);

        /// <summary>
        /// Obtiene un usuario por su correo electrónico.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <returns>Usuario si existe, o null.</returns>
        Task<Usuario?> GetUsuarioByCorreoAsync(string correo);

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="usuario">Objeto usuario a registrar.</param>
        /// <returns>True si se creó correctamente, false si ya existe.</returns>
        Task<bool> PostUsuarios(Usuario usuario);

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="usuario">Usuario con datos actualizados.</param>
        /// <returns>True si se actualizó, false si no se encontró.</returns>
        Task<bool> PutUsuarios(Usuario usuario);

        /// <summary>
        /// Elimina un usuario por ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>True si se eliminó, false si no se encontró.</returns>
        Task<bool> DeleteUsuarios(int id);

        /// <summary>
        /// Asigna un nuevo rol a un usuario.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <param name="nuevoRolId">ID del nuevo rol.</param>
        /// <returns>Usuario con el nuevo rol asignado.</returns>
        Task<Usuario?> AsignarRol(int id, int nuevoRolId);

        /// <summary>
        /// Guarda un token temporal de recuperación de contraseña para un usuario.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <param name="token">Código/token a guardar.</param>
        /// <returns>True si se guardó correctamente.</returns>
        Task<bool> GuardarTokenRecuperacionAsync(int usuarioId, string token);

        /// <summary>
        /// Obtiene un usuario a partir de su token de recuperación.
        /// </summary>
        /// <param name="token">Token de recuperación.</param>
        /// <returns>Usuario si el token es válido, o null.</returns>
        Task<Usuario?> GetUsuarioPorTokenAsync(string token);

        /// <summary>
        /// Elimina el token de recuperación una vez usado.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <returns>True si se eliminó correctamente.</returns>
        Task<bool> LimpiarTokenRecuperacionAsync(int usuarioId);

        /// <summary>
        /// Envía un correo con el código de recuperación.
        /// </summary>
        /// <param name="destino">Correo del destinatario.</param>
        /// <param name="codigo">Código que se enviará.</param>
        /// <returns>True si el correo fue enviado exitosamente.</returns>
        Task<bool> EnviarCorreo(string destino, string codigo);
        Task<List<Rol>> GetRolesAsync();

        Task<Usuario> GetByIdAsync(int id);
        Task UpdateAsync(Usuario usuario);

    }
}
