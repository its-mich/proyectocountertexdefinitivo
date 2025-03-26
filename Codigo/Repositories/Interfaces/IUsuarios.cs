using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IUsuarios
    {
        Task<List<Usuario>> GetUsuarios();
        Task<bool> PostUsuarios(Usuario usuarios);
        Task<bool> PutUsuarios(Usuario usuarios);
     
        Task<bool> DeleteUsuarios(int id);
    }
}
