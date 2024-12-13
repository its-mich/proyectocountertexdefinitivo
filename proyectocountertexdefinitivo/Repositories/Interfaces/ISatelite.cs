using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface ISatelite
    {

        Task<List<Satelite>> GetSatelite();
        Task<bool> PostSatelite(Satelite satelite);
        Task<bool> PutSatelite(Satelite satelite);
        Task<bool> DeleteSatelite( int id);

    }

}
