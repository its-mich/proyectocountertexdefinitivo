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
                return await _context.Produccion.ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Produccion>> GetProduccion(int id)
            {
                var produccion = await _context.Produccion.FindAsync(id);

                if (produccion == null)
                {
                    return NotFound();
                }

                return produccion;
            }

            [HttpPost]
            public async Task<ActionResult<Produccion>> PostProduccion(Produccion produccion)
            {
                _context.Produccion.Add(produccion);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProduccion", new { id = produccion.Id }, produccion);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteProduccion(int id)
            {
                var produccion = await _context.Produccion.FindAsync(id);
                if (produccion == null)
                {
                    return NotFound();
                }

                _context.Produccion.Remove(produccion);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }

    
}
