using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using Microsoft.AspNetCore.Authorization; // Necesario para [AllowAnonymous]

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken] // <--- ¡AÑADIR ESTO AQUÍ!
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
                    Usuario = p.Usuario.Nombre,
                    Prenda = p.Prenda.Nombre,
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
        [AllowAnonymous]
        public async Task<ActionResult<Produccion>> PostProduccion(Produccion produccion)
        {
            // ... (tu código de validación y guardado actual) ...
            if (produccion.UsuarioId <= 0 || produccion.PrendaId <= 0)
            {
                ModelState.AddModelError(nameof(produccion.UsuarioId), "El usuario es obligatorio.");
                ModelState.AddModelError(nameof(produccion.PrendaId), "La prenda es obligatoria.");
                return BadRequest(ModelState);
            }

            var usuario = await _context.Usuarios.FindAsync(produccion.UsuarioId);
            var prenda = await _context.Prendas.FindAsync(produccion.PrendaId);

            if (usuario == null)
            {
                ModelState.AddModelError(nameof(produccion.UsuarioId), "El usuario seleccionado no existe.");
                return BadRequest(ModelState);
            }
            if (prenda == null)
            {
                ModelState.AddModelError(nameof(produccion.PrendaId), "La prenda seleccionada no existe.");
                return BadRequest(ModelState);
            }

            if (produccion.ProduccionDetalles == null || !produccion.ProduccionDetalles.Any())
            {
                ModelState.AddModelError(nameof(produccion.ProduccionDetalles), "Debe agregar al menos un detalle de producción.");
                return BadRequest(ModelState);
            }

            int detalleIndex = 0;
            foreach (var detalle in produccion.ProduccionDetalles)
            {
                if (detalle.Cantidad <= 0)
                {
                    ModelState.AddModelError($"{nameof(produccion.ProduccionDetalles)}[{detalleIndex}].{nameof(detalle.Cantidad)}", "La cantidad del detalle debe ser mayor que cero.");
                }
                if (detalle.OperacionId <= 0)
                {
                    ModelState.AddModelError($"{nameof(produccion.ProduccionDetalles)}[{detalleIndex}].{nameof(detalle.OperacionId)}", "La operación del detalle es obligatoria.");
                }
                var operacion = await _context.Operaciones.FindAsync(detalle.OperacionId);
                if (operacion == null)
                {
                    ModelState.AddModelError($"{nameof(produccion.ProduccionDetalles)}[{detalleIndex}].{nameof(detalle.OperacionId)}", "La operación seleccionada en el detalle no existe.");
                }
                detalleIndex++;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Producciones.Add(produccion);
            await _context.SaveChangesAsync();

            foreach (var detalle in produccion.ProduccionDetalles)
            {
                detalle.ProduccionId = produccion.Id;
                _context.ProduccionDetalle.Add(detalle);
            }
            await _context.SaveChangesAsync();

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

            var detalles = await _context.ProduccionDetalle
                                         .Where(d => d.ProduccionId == id)
                                         .ToListAsync();

            _context.ProduccionDetalle.RemoveRange(detalles);

            _context.Producciones.Remove(produccion);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
