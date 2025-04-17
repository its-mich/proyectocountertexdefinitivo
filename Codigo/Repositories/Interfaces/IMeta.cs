using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IMeta
    {
        Task<IEnumerable<Meta>> GetAllAsync();
        Task<Meta> GetByIdAsync(int id);
        Task<Meta> CreateAsync(Meta meta);
        Task DeleteAsync(int id);
    }
}
