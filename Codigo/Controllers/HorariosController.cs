using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador que gestiona la asignación y consulta de horarios de los empleados.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de la base de datos de CounterTex.</param>
        public HorariosController(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de todos los horarios registrados.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="HorarioDTO"/> con información del horario y empleado.</returns>
        /// <response code="200">Lista obtenida correctamente.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioDTO>>> GetHorarios()
        {
            var horarios = await _context.Horarios
                .Include(h => h.Usuario)
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

        /// <summary>
        /// Obtiene los detalles de un horario específico por su ID.
        /// </summary>
        /// <param name="id">Identificador del horario.</param>
        /// <returns>Un objeto <see cref="HorarioDTO"/> si existe, de lo contrario, <see cref="NotFound"/>.</returns>
        /// <response code="200">Horario encontrado.</response>
        /// <response code="404">Horario no encontrado.</response>
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

        /// <summary>
        /// Crea un nuevo registro de horario para un empleado.
        /// </summary>
        /// <param name="horarioDto">Objeto <see cref="HorarioDTO"/> con la información del horario.</param>
        /// <returns>El horario creado.</returns>
        /// <response code="201">Horario creado correctamente.</response>
        /// <response code="404">Usuario no encontrado.</response>
        /// <response code="409">Ya existe un horario para esa fecha, tipo y empleado.</response>
        [HttpPost]
        public async Task<ActionResult<HorarioDTO>> PostHorario(HorarioDTO horarioDto)
        {
            var usuario = await _context.Usuarios.FindAsync(horarioDto.EmpleadoId);
            if (usuario == null)
                return NotFound($"No se encontró un usuario con ID {horarioDto.EmpleadoId}.");

            var horarioExistente = await _context.Horarios.FirstOrDefaultAsync(h =>
                h.EmpleadoId == horarioDto.EmpleadoId &&
                h.Fecha.Date == horarioDto.Fecha.Date &&
                h.Tipo.ToLower() == horarioDto.Tipo.ToLower());

            if (horarioExistente != null)
                return Conflict("Ya existe un horario para ese empleado, fecha y tipo.");

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

        /// <summary>
        /// Elimina un horario existente por su ID.
        /// </summary>
        /// <param name="id">Identificador del horario.</param>
        /// <returns>Respuesta 204 si se eliminó correctamente o 404 si no se encontró.</returns>
        /// <response code="204">Horario eliminado correctamente.</response>
        /// <response code="404">Horario no encontrado.</response>
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
