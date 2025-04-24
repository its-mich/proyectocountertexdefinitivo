using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IHorario
    {
        Task<IEnumerable<Horario>> GetAllAsync();
        Task<Horario> GetByIdAsync(int id);
        Task<Horario> CreateAsync(Horario horario);
        Task DeleteAsync(int id);
    }
}
