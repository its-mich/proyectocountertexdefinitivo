using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Controllers
{
  
        [Route("api/[controller]")]
        [ApiController]
        public class MetasController : ControllerBase
        {
            private readonly CounterTexDBContext _context;

            public MetasController(CounterTexDBContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Meta>>> GetMetas()
            {
                return await _context.Metas.ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Meta>> GetMeta(int id)
            {
                var meta = await _context.Metas.FindAsync(id);

                if (meta == null)
                {
                    return NotFound();
                }

                return meta;
            }

            [HttpPost]
            public async Task<ActionResult<Meta>> PostMeta(Meta meta)
            {
                _context.Metas.Add(meta);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMeta", new { id = meta.Id }, meta);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteMeta(int id)
            {
                var meta = await _context.Metas.FindAsync(id);
                if (meta == null)
                {
                    return NotFound();
                }

                _context.Metas.Remove(meta);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }

    
}
