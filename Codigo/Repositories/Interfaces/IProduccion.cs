using proyectocountertexdefinitivo.Controllers;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IProduccion
    {
        Task<IEnumerable<Produccion>> GetAllAsync();
        Task<Produccion> GetByIdAsync(int id);
        Task<Produccion> CreateAsync(Produccion produccion);
        Task DeleteAsync(int id);
    }
}
