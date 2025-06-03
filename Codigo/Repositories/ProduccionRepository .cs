using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Controllers;
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

        public async Task<IEnumerable<Produccion>> GetAllAsync() => await _context.Producciones.ToListAsync();

        public async Task<Produccion> GetByIdAsync(int id) => await _context.Producciones.FindAsync(id);

        public async Task<Produccion> CreateAsync(Produccion produccion)
        {
            _context.Producciones.Add(produccion);
            await _context.SaveChangesAsync();
            return produccion;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var produccion = await _context.Producciones.FindAsync(id);
            if (produccion != null)
            {
                _context.Producciones.Remove(produccion);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
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
    }
}
