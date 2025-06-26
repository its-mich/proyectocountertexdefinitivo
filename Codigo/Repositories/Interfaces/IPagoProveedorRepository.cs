using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IPagoProveedorRepository
    {
        Task<List<PagoProveedor>> ObtenerPorProveedorAsync(int proveedorId);
        Task AgregarPagoProveedorAsync(PagoProveedor pago);
    }
}
