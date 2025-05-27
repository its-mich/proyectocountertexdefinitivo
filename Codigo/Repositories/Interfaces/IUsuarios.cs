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
    }
}
