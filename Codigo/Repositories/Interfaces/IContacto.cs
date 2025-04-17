using proyectocountertexdefinitivo.Controllers;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IContacto
    {

        Task<IEnumerable<Contacto>> GetAllAsync();
        Task<Contacto> GetByIdAsync(int id);
        Task<Contacto> CreateAsync(Contacto contacto);
        Task DeleteAsync(int id);
    }
}
