using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduccionController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        public ProduccionController(CounterTexDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProduccionDto>>> GetProduccion()
        {
            var producciones = await _context.Producciones
                .Include(p => p.Usuario)
                .Include(p => p.Prenda)
                .Select(p => new ProduccionDto
                {
                    Id = p.Id,
                    Fecha = p.Fecha,
                    Usuario = p.Usuario.Nombre, // o como se llame
                    Prenda = p.Prenda.Nombre,   // igual aquí
                    Total = p.ProduccionDetalles.Sum(d => d.Cantidad)
                })
                .ToListAsync();

            return Ok(producciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produccion>> GetProduccion(int id)
        {
            var produccion = await _context.Producciones.FindAsync(id);

            if (produccion == null)
            {
                return NotFound();
            }

            return produccion;
        }

        [HttpPost]
        public async Task<ActionResult<Produccion>> PostProduccion(Produccion produccion)
        {
            // Validación de los campos requeridos
            if (produccion.UsuarioId <= 0 || produccion.PrendaId <= 0)
            {
                return BadRequest("El usuario y la prenda deben ser válidos.");
            }

            // Verificar que el Usuario y la Prenda existan
            var usuario = await _context.Usuarios.FindAsync(produccion.UsuarioId);
            var prenda = await _context.Prendas.FindAsync(produccion.PrendaId);

            if (usuario == null || prenda == null)
            {
                return BadRequest("El usuario o la prenda no existen.");
            }

            // Añadir la producción a la base de datos
            _context.Producciones.Add(produccion);
            await _context.SaveChangesAsync();

            // Asegurar que los detalles de producción también se guarden correctamente
            foreach (var detalle in produccion.ProduccionDetalles)
            {
                detalle.ProduccionId = produccion.Id;  // Asignar el ID de la producción
                _context.ProduccionDetalle.Add(detalle);
            }

            // Guardar los detalles de producción
            await _context.SaveChangesAsync();

            // Devolver la respuesta con la producción creada
            return CreatedAtAction(nameof(GetProduccion), new { id = produccion.Id }, produccion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduccion(int id)
        {
            var produccion = await _context.Producciones.FindAsync(id);
            if (produccion == null)
            {
                return NotFound();
            }

            // Eliminar primero los detalles relacionados
            var detalles = await _context.ProduccionDetalle
                                         .Where(d => d.ProduccionId == id)
                                         .ToListAsync();

            _context.ProduccionDetalle.RemoveRange(detalles);

            // Ahora sí, eliminar la producción
            _context.Producciones.Remove(produccion);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}