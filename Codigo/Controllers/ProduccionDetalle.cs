using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace  proyectocountertexdefinitivo.Controllers

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
            return await _context.ProduccionDetalle.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ProduccionDetalle>> PostProduccionDetalle(ProduccionDetalle produccionDetalle)
        {
            _context.ProduccionDetalle.Add(produccionDetalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduccionDetalle", new { id = produccionDetalle.Id }, produccionDetalle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduccionDetalle(int id)
        {
            var produccionDetalle = await _context.ProduccionDetalle.FindAsync(id);
            if (produccionDetalle == null)
            {
                return NotFound();
            }

            _context.ProduccionDetalle.Remove(produccionDetalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }


}






