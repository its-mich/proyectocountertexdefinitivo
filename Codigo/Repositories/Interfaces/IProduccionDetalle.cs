using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para las operaciones CRUD sobre la entidad ProduccionDetalle.
    /// </summary>
    public interface IProduccionDetalle
    {
        Task<IEnumerable<ProduccionDetalle>> GetAllAsync();
        Task<ProduccionDetalle> GetByIdAsync(int id);
        Task<ProduccionDetalle> CreateAsync(ProduccionDetalle detalle);
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Crea un nuevo detalle de producción calculando automáticamente el ValorTotal.
        /// </summary>
        /// <param name="detalle">Detalle de producción con OperacionId y Cantidad.</param>
        /// <returns>Detalle de producción creado con el ValorTotal calculado o null si ocurre un error.</returns>
        Task<ProduccionDetalle?> CrearConCalculoAsync(ProduccionDetalle detalle);

        Task<IEnumerable<ProduccionDetalle>> ObtenerDetallesPorEmpleadoAsync(int usuarioId);
    }
}
