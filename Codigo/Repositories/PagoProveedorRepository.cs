using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace proyectocountertexdefinitivo.Repositories
{
    public class PagoProveedorRepository : IPagoProveedorRepository
    {

        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto de la base de datos.</param>
        public PagoProveedorRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task AgregarPagoProveedorAsync(PagoProveedor pago)
        {
            _context.PagosProveedor.Add(pago);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PagoProveedor>> ObtenerPorProveedorAsync(int proveedorId)
        {
            return await _context.PagosProveedor
                .Where(p => p.ProveedorId == proveedorId)
                .OrderByDescending(p => p.FechaRegistro)
                .ToListAsync();
        }
    }
}
