using proyectocountertexdefinitivo.Controllers;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IProduccion
    {
        Task<IEnumerable<Produccion>> GetAllAsync();
        Task<Produccion> GetByIdAsync(int id);
        Task<Produccion> CreateAsync(Produccion produccion);

        /// <summary>
        /// Elimina una producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la producción a eliminar.</param>
        Task<bool> DeleteAsync(int id);

        Task<object> ObtenerResumenMensual(int anio, int mes);
    }
}
