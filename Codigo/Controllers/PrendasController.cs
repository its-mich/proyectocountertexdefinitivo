using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrendasController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        public PrendasController(CounterTexDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prenda>>> GetPrendas()
        {
            return await _context.Prendas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prenda>> GetPrenda(int id)
        {
            var prenda = await _context.Prendas.FindAsync(id);

            if (prenda == null)
            {
                return NotFound();
            }

            return prenda;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrenda(int id, Prenda prenda)
        {
            if (id != prenda.Id)
            {
                return BadRequest();
            }

            _context.Entry(prenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Prenda>> PostPrenda(Prenda prenda)
        {
            _context.Prendas.Add(prenda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrenda", new { id = prenda.Id }, prenda);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrenda(int id)
        {
            var prenda = await _context.Prendas.FindAsync(id);
            if (prenda == null)
            {
                return NotFound();
            }

            _context.Prendas.Remove(prenda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrendaExists(int id)
        {
            return _context.Prendas.Any(e => e.Id == id);
        }
    }

}
