using proyectocountertexdefinitivo.Models;


namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IProduccionDetalle
    {
        Task<IEnumerable<ProduccionDetalle>> GetAllAsync();
        Task<ProduccionDetalle> GetByIdAsync(int id);
        Task<ProduccionDetalle> CreateAsync(ProduccionDetalle detalle);
        Task DeleteAsync(int id);
    }
}
