using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IPagoRepository
    {
        Task<bool> GenerarPagoQuincenalAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<List<Pago>> ObtenerPagosAsync();

        Task<List<Pago>> ObtenerPagosPorUsuarioAsync(int usuarioId);
    }
}
