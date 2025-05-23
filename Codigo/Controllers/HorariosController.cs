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

        // GET: api/Horarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioDTO>>> GetHorarios()
        {
            var horarios = await _context.Horarios
                .Include(h => h.Usuario) // Asegura que incluya los datos del usuario relacionado
                .Select(h => new HorarioDTO
                {
                    EmpleadoId = h.EmpleadoId,
                    NombreEmpleado = h.Usuario.Nombre,
                    Fecha = h.Fecha,
                    Tipo = h.Tipo,
                    Hora = h.Hora,
                    Observaciones = h.Observaciones
                })
                .ToListAsync();

            return Ok(horarios);
        }

        // GET: api/Horarios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHorarioPorId(int id)
        {
            var horario = await _context.Horarios
                .Include(h => h.Usuario)
                .FirstOrDefaultAsync(h => h.HorarioId == id);

            if (horario == null)
                return NotFound();

            var horarioDto = new HorarioDTO
            {
                EmpleadoId = horario.EmpleadoId,
                NombreEmpleado = horario.Usuario?.Nombre,
                Fecha = horario.Fecha,
                Tipo = horario.Tipo,
                Hora = horario.Hora,
                Observaciones = horario.Observaciones
            };

            return Ok(horarioDto);
        }

        // POST: api/Horarios
        [HttpPost]
        public async Task<ActionResult<HorarioDTO>> PostHorario(HorarioDTO horarioDto)
        {
            // 1. Verificar que el empleado exista
            var usuario = await _context.Usuarios.FindAsync(horarioDto.EmpleadoId);
            if (usuario == null)
            {
                return NotFound($"No se encontró un usuario con ID {horarioDto.EmpleadoId}.");
            }

            // 2. Validar si ya existe el horario con la clave compuesta
            var horarioExistente = await _context.Horarios.FirstOrDefaultAsync(h =>
            h.EmpleadoId == horarioDto.EmpleadoId &&
            h.Fecha.Date == horarioDto.Fecha.Date &&
            h.Tipo.ToLower() == horarioDto.Tipo.ToLower()
);

            if (horarioExistente != null)
            {
                return Conflict("Ya existe un horario para ese empleado, fecha y tipo.");
            }

            // 3. Mapear DTO a entidad Horario
            var horario = new Horario
            {
                EmpleadoId = horarioDto.EmpleadoId,
                Fecha = horarioDto.Fecha.Date,
                Tipo = horarioDto.Tipo.ToLower(),
                Hora = horarioDto.Hora,
                Observaciones = horarioDto.Observaciones
            };

            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            // 4. Devolver DTO de confirmación (puedes incluir nombre si quieres)
            var resultado = new HorarioDTO
            {
                EmpleadoId = horario.EmpleadoId,
                NombreEmpleado = usuario.Nombre,
                Fecha = horario.Fecha,
                Tipo = horario.Tipo,
                Hora = horario.Hora,
                Observaciones = horario.Observaciones
            };

            return CreatedAtAction(nameof(GetHorarios), new { id = horario.EmpleadoId }, resultado);
        }

        // DELETE: api/Horarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
                return NotFound();

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}




