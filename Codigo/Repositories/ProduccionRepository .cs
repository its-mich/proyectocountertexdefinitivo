using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories.repositories
{
    /// <summary>
    /// Repositorio que gestiona las operaciones CRUD para la entidad <see cref="Produccion"/>.
    /// </summary>
    public class ProduccionRepository : IProduccion
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto <see cref="CounterTexDBContext"/>.</param>
        public ProduccionRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las producciones registradas.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="Produccion"/>.</returns>
        public async Task<IEnumerable<Produccion>> GetAllAsync()
        {
            return await _context.Producciones.ToListAsync();
        }

        /// <summary>
        /// Obtiene una producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la producción.</param>
        /// <returns>El objeto <see cref="Produccion"/> correspondiente o null si no existe.</returns>
        public async Task<Produccion> GetByIdAsync(int id)
        {
            return await _context.Producciones.FindAsync(id);
        }

        /// <summary>
        /// Crea una nueva producción en la base de datos.
        /// </summary>
        /// <param name="produccion">Objeto <see cref="Produccion"/> a registrar.</param>
        /// <returns>El objeto <see cref="Produccion"/> creado.</returns>
        public async Task<Produccion> CreateAsync(Produccion produccion)
        {
            _context.Producciones.Add(produccion);
            await _context.SaveChangesAsync();
            return produccion;
        }

        /// <summary>
        /// Actualiza una producción existente.
        /// </summary>
        /// <param name="produccion">Objeto <see cref="Produccion"/> con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, false en caso contrario.</returns>
        public async Task<bool> UpdateAsync(Produccion produccion)
        {
            var existing = await _context.Producciones.FindAsync(produccion.Id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(produccion);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Elimina una producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la producción a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var produccion = await _context.Producciones.FindAsync(id);
            if (produccion == null)
            {
                return false;
            }

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
    }
}
