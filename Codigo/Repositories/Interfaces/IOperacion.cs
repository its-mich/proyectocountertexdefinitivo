using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IOperacion
    {
        Task<IEnumerable<Operacion>> GetAllAsync();
        Task<Operacion> GetByIdAsync(int id);
        Task<Operacion> CreateAsync(Operacion operacion);
        Task UpdateAsync(Operacion operacion);
        Task DeleteAsync(int id);
    }
}
