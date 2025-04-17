using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IRegistro
    {
        Task<List<Registros>> GetRegistro();
        Task<bool> PostRegistro(Registros registro);
        Task<bool> PutRegistro(Registros registro);
        Task<bool> DeleteRegistro(int id);
    }
}
