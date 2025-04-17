using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Controllers
{
    public class MensajesChat : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class MensajesChatController : ControllerBase
        {
            private readonly CounterTexDBContext _context;

            public MensajesChatController(CounterTexDBContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<MensajeChat>>> GetMensajesChat()
            {
                return await _context.MensajesChat.ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<MensajeChat>> GetMensajeChat(int id)
            {
                var mensajeChat = await _context.MensajesChat.FindAsync(id);

                if (mensajeChat == null)
                {
                    return NotFound();
                }

                return mensajeChat;
            }

            [HttpPost]
            public async Task<ActionResult<MensajeChat>> PostMensajeChat(MensajeChat mensajeChat)
            {
                _context.MensajesChat.Add(mensajeChat);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMensajeChat", new { id = mensajeChat.Id }, mensajeChat);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteMensajeChat(int id)
            {
                var mensajeChat = await _context.MensajesChat.FindAsync(id);
                if (mensajeChat == null)
                {
                    return NotFound();
                }

                _context.MensajesChat.Remove(mensajeChat);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }

    }
}
