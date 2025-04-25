using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperacionController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        public OperacionController(CounterTexDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operacion>>> GetOperacion()
        {
            return await _context.Operaciones.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Operacion>> GetOperacion(int id)
        {
            var operacion = await _context.Operaciones.FindAsync(id);

            if (operacion == null)
            {
                return NotFound();
            }

            return operacion;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperacion(int id, OperacionUpdateDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var operacion = await _context.Operaciones.FindAsync(id);
            if (operacion == null)
                return NotFound();

            operacion.Nombre = dto.Nombre;
            operacion.ValorUnitario = dto.ValorUnitario;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostOperacion(OperacionCreateDTO dto)
        {
            var operacion = new Operacion
            {
                Nombre = dto.Nombre,
                ValorUnitario = dto.ValorUnitario
            };

            _context.Operaciones.Add(operacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOperacion), new { id = operacion.Id }, operacion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperacion(int id)
        {
            var operacion = await _context.Operaciones.FindAsync(id);
            if (operacion == null)
            {
                return NotFound();
            }

            _context.Operaciones.Remove(operacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperacionExists(int id)
        {
            return _context.Operaciones.Any(e => e.Id == id);
        }
    }


}
