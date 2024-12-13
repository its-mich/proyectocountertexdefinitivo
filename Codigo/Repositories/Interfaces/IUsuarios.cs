using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IUsuarios
    {
        Task<List<Usuarios>> GetUsuarios();
        Task<bool> PostUsuarios(Usuarios usuarios);
        Task<bool> PutUsuarios(Usuarios usuarios);
     
        Task<bool> DeleteUsuarios(int id);
    }
}
