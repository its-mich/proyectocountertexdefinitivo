using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IPrenda
    {

        Task<IEnumerable<Prenda>> GetAllAsync();
        Task<Prenda> GetByIdAsync(int id);
        Task<Prenda> CreateAsync(Prenda prenda);
        Task UpdateAsync(Prenda prenda);
        Task DeleteAsync(int id);

    }

}
