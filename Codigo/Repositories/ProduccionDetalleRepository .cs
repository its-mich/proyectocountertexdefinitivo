using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories.repositories
{
    /// <summary>
    /// Repositorio que gestiona las operaciones CRUD para la entidad <see cref="ProduccionDetalle"/>.
    /// </summary>
    public class ProduccionDetalleRepository : IProduccionDetalle
    {
        private readonly CounterTexDBContext _context;

        public ProduccionDetalleRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProduccionDetalle>> GetAllAsync()
        {
            return await _context.ProduccionDetalles.ToListAsync();
        }

        public async Task<ProduccionDetalle> GetByIdAsync(int id)
        {
            return await _context.ProduccionDetalles.FindAsync(id);
        }

        public async Task<ProduccionDetalle> CreateAsync(ProduccionDetalle detalle)
        {
            _context.ProduccionDetalles.Add(detalle);
            await _context.SaveChangesAsync();
            return detalle;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var detalle = await _context.ProduccionDetalles.FindAsync(id);
            if (detalle != null)
            {
                _context.ProduccionDetalles.Remove(detalle);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Crea un detalle de producción calculando el valor total automáticamente desde el valor de la operación.
        /// </summary>
        /// <param name="detalle">Detalle con OperacionId y Cantidad.</param>
        /// <returns>El detalle creado o null si no se encuentra la operación.</returns>
        public async Task<ProduccionDetalle?> CrearConCalculoAsync(ProduccionDetalle detalle)
        {
            // Validar que la operación exista
            var operacion = await _context.Operaciones.FindAsync(detalle.OperacionId);
            if (operacion == null)
                return null;

            // Calcular valor total
            detalle.ValorTotal = detalle.Cantidad * operacion.ValorUnitario;

            // Guardar el detalle
            _context.ProduccionDetalles.Add(detalle);
            await _context.SaveChangesAsync();

            return detalle;
        }
    }
}
