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
        public async Task<ActionResult<IEnumerable<HorarioDTO>>> GetHorarios()
        {
            var horarios = await _context.Horarios.ToListAsync();

            var horariosDto = horarios.Select(h => new HorarioDTO
            {
                EmpleadoId = h.EmpleadoId,
                Tipo = h.Tipo,
                Hora = h.Hora,
                Fecha = h.Fecha,
                Observaciones = h.Observaciones
            }).ToList();

            return horariosDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioDTO>> GetHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);

            if (horario == null)
            {
                return NotFound();
            }

            var horarioDto = new HorarioDTO
            {
                EmpleadoId = horario.EmpleadoId,
                Tipo = horario.Tipo,
                Hora = horario.Hora,
                Fecha = horario.Fecha,
                Observaciones = horario.Observaciones
            };

            return horarioDto;
        }

        // PUT: api/Horarios/1/2025-05-15/entrada
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, HorarioDTO horarioDto)
        {
            if (id != horarioDto.EmpleadoId)
            {
                return BadRequest("El ID en la URL no coincide con el EmpleadoId en el cuerpo.");
            }

            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound($"No se encontró horario con EmpleadoId {id}");
            }

            // Actualizar campos
            horario.Tipo = horarioDto.Tipo;
            horario.Hora = horarioDto.Hora;
            horario.Fecha = horarioDto.Fecha;
            horario.Observaciones = horarioDto.Observaciones;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Horarios.Any(e => e.EmpleadoId == id))
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
        public async Task<ActionResult<HorarioDTO>> PostHorario(HorarioDTO horarioDto)
        {
            // Mapear DTO a entidad Horario
            var horario = new Horario
            {
                EmpleadoId = horarioDto.EmpleadoId,
                Tipo = horarioDto.Tipo,
                Hora = horarioDto.Hora,
                Fecha = horarioDto.Fecha,
                Observaciones = horarioDto.Observaciones
            };
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            // Mapear de nuevo a DTO para devolver solo lo necesario
            var resultadoDto = new HorarioDTO
            {
                EmpleadoId = horario.EmpleadoId,
                Tipo = horario.Tipo,
                Hora = horario.Hora,
                Fecha = horario.Fecha,
                Observaciones = horario.Observaciones
            };

            return CreatedAtAction("GetHorario", new { id = horario.EmpleadoId }, resultadoDto);
        }

        // DELETE: api/Horarios/1/2025-05-15/entrada
        [HttpDelete("{empleadoId:int}/{fecha:datetime}/{tipo}")]
        public async Task<IActionResult> DeleteHorario(int empleadoId, DateTime fecha, string tipo)
        {
            var horario = await _context.Horarios.FindAsync(empleadoId, fecha, tipo);
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




