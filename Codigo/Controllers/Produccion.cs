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
        public async Task<ActionResult<IEnumerable<Produccion>>> GetProduccion()
        {
            return await _context.Producciones.ToListAsync();
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

            _context.Producciones.Remove(produccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
