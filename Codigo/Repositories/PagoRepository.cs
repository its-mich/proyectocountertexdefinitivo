using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto de la base de datos.</param>
        public PagoRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<bool> GenerarPagoQuincenalAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var query = $"EXEC sp_GenerarPagoQuincenal @p0, @p1";
            await _context.Database.ExecuteSqlRawAsync(query, fechaInicio, fechaFin);
            return true;
        }

        public async Task<List<Pago>> ObtenerPagosAsync()
        {
            return await _context.Pagos
                .Include(p => p.Usuario)
                .OrderByDescending(p => p.FechaPago)
                .ToListAsync();
        }

        public async Task<List<Pago>> ObtenerPagosPorUsuarioAsync(int usuarioId)
        {
            return await _context.Pagos
                .Include(p => p.Usuario)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.FechaPago)
                .ToListAsync();
        }

    }
}
