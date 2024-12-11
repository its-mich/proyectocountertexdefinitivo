using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IEmpleado
    {
        Task<List<PerfilEmpleado>> GetEmpleado();
        Task<bool> PostEmpleado(PerfilEmpleado perfilEmpleado);
        Task<bool> PutEmpleado(PerfilEmpleado perfilEmpleado);
        Task<bool> DeleteEmpleado(PerfilEmpleado perfilEmpleado);
        Task<bool> DeleteEmpleado(int id);
    }
}
