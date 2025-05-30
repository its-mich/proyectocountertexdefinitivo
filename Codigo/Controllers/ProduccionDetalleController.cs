using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduccionDetalleController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        public ProduccionDetalleController(CounterTexDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProduccionDetalle>>> GetProduccionDetalles()
        {
            return await _context.ProduccionDetalles.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ProduccionDetalle>> PostProduccionDetalle(ProduccionDetalle produccionDetalle)
        {
            _context.ProduccionDetalles.Add(produccionDetalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduccionDetalle", new { id = produccionDetalle.Id }, produccionDetalle);
        }

        // ✅ Método nuevo que calcula ValorTotal antes de guardar
        [HttpPost("crear-con-calculo")]
        public async Task<ActionResult<ProduccionDetalle>> CrearDetalleConValorTotal(ProduccionDetalle detalle)
        {
            var operacion = await _context.Operaciones
                .FirstOrDefaultAsync(o => o.Id == detalle.OperacionId);

            if (operacion == null)
            {
                return BadRequest("Operación no encontrada.");
            }

            detalle.ValorTotal = detalle.Cantidad * operacion.ValorUnitario.Value;

            _context.ProduccionDetalles.Add(detalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduccionDetalles), new { id = detalle.Id }, detalle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduccionDetalle(int id)
        {
            var produccionDetalle = await _context.ProduccionDetalles.FindAsync(id);
            if (produccionDetalle == null)
            {
                return NotFound();
            }

            _context.ProduccionDetalles.Remove(produccionDetalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
