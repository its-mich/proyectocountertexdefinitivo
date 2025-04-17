using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IMensajesChat
    {
        Task<IEnumerable<MensajeChat>> ObtenerTodosAsync();
        Task<MensajeChat> ObtenerPorIdAsync(int id);
        Task<IEnumerable<MensajeChat>> ObtenerPorUsuarioAsync(int usuarioId);
        Task AgregarAsync(MensajeChat mensaje);
        Task ActualizarAsync(MensajeChat mensaje);
        Task EliminarAsync(int id);

    }
}
