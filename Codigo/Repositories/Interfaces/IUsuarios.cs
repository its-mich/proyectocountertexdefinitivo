using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IUsuarios
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task<Usuario> GetUsuarioByCorreoAsync(string correo);  // Obtener un usuario por correo
        Task<Usuario> GetUsuarioByCodigoAsync(string codigo); // Obtener un usuario por código de verificación
        Task<bool> UpdateUsuarioAsync(Usuario usuario);       // Nuevo método para actualizar un usuario
    }
}
