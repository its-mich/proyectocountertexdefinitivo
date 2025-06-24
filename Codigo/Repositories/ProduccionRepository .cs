using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories.repositories
{
    public class ProduccionRepository : IProduccion
    {
        private readonly CounterTexDBContext _context;

        public ProduccionRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            return await _context.Producciones
                .Include(p => p.Usuario)
                .Include(p => p.Prenda)
                .Include(p => p.ProduccionDetalles)
                    .ThenInclude(d => d.Operacion)
                .Select(p => new
                {
                    p.Id,
                    p.Fecha,
                    p.TotalValor,
                    p.UsuarioId,
                    p.PrendaId,
                    NombreUsuario = p.Usuario.Nombre,
                    NombrePrenda = p.Prenda.Nombre,
                    ProduccionDetalles = p.ProduccionDetalles.Select(d => new
                    {
                        d.Id,
                        d.ProduccionId,
                        d.Cantidad,
                        d.OperacionId,
                        d.ValorTotal,
                        NombreOperacion = d.Operacion.Nombre,
                        ValorOperacion = d.ValorTotal
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Produccion> GetByIdAsync(int id)
        {
            return await _context.Producciones.FindAsync(id);
        }

        public async Task<Operacion?> ObtenerOperacionPorId(int operacionId)
        {
            return await _context.Operaciones.FindAsync(operacionId);
        }

        public async Task<Produccion> CreateAsync(Produccion produccion)
        {
            _context.Producciones.Add(produccion);
            await _context.SaveChangesAsync();
            return produccion;
        }

        public async Task<bool> UpdateAsync(Produccion produccion)
        {
            var existing = await _context.Producciones.FindAsync(produccion.Id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(produccion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var produccion = await _context.Producciones
                .Include(p => p.ProduccionDetalles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produccion == null)
                return false;


            _context.ProduccionDetalles.RemoveRange(produccion.ProduccionDetalles);
            _context.Producciones.Remove(produccion);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<object> ObtenerResumenMensual(int anio, int mes)
        {
            var resumen = await _context.ProduccionDetalles
                .Where(d => d.Produccion.Fecha.Year == anio && d.Produccion.Fecha.Month == mes)
                .GroupBy(d => d.Produccion.Prenda.Nombre)
                .Select(g => new
                {
                    Prenda = g.Key,
                    Total = g.Sum(d => d.Cantidad)
                })
                .ToListAsync();

            return resumen.Any() ? resumen : null;
        }
        public async Task<Produccion?> CrearProduccionConDetallesAsync(Produccion produccion)
        {
            if (produccion == null || produccion.ProduccionDetalles == null || !produccion.ProduccionDetalles.Any())
                return null;

            decimal totalProduccion = 0;

            foreach (var detalle in produccion.ProduccionDetalles)
            {
                var operacion = await _context.Operaciones.FindAsync(detalle.OperacionId);
                if (operacion == null)
                    return null;

                detalle.ValorTotal = operacion.ValorUnitario * detalle.Cantidad;
                totalProduccion += detalle.ValorTotal ?? 0;
            }

            produccion.TotalValor = totalProduccion;

            // Se agrega la producción con sus detalles en cascada
            _context.Producciones.Add(produccion);
            await _context.SaveChangesAsync();

            return produccion;
        }

        // En ProduccionRepository.cs
        public async Task<IEnumerable<Produccion>> GetProduccionesPorUsuarioIdAsync(int usuarioId)
        {
            return await _context.Producciones
                .Where(p => p.UsuarioId == usuarioId)
                .Include(p => p.Prenda)
                .Include(p => p.ProduccionDetalles)
                    .ThenInclude(d => d.Operacion)
                .ToListAsync();
        }

    }
}