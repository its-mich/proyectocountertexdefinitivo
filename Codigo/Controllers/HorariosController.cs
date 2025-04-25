using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        public HorariosController(CounterTexDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horario>>> GetHorarios()
        {
            return await _context.Horarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Horario>> GetHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);

            if (horario == null)
            {
                return NotFound();
            }

            return horario;
        }

        [HttpPost]
        public async Task<ActionResult<Horario>> PostHorario(Horario horario)
        {
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHorario", new { id = horario.Id }, horario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}




