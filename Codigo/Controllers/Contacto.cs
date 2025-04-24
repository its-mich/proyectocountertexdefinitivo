using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class ContactoController : ControllerBase
        {
            private readonly CounterTexDBContext _context;

            public ContactoController(CounterTexDBContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Contacto>>> GetContactos()
            {
                return await _context.Contacto.ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Contacto>> GetContacto(int id)
            {
                var contacto = await _context.Contacto.FindAsync(id);

                if (contacto == null)
                {
                    return NotFound();
                }
                  
                return contacto;
            }

            [HttpPost]
            public async Task<ActionResult<Contacto>> PostContacto(Contacto contacto)
            {
                _context.Contacto.Add(contacto);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetContacto", new { id = contacto.Id }, contacto);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteContacto(int id)
            {
                var contacto = await _context.Contacto.FindAsync(id);
                if (contacto == null)
                {
                    return NotFound();
                }

                _context.Contacto.Remove(contacto);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }

    
}
