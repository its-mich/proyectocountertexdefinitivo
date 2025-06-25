using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar los horarios en el sistema CounterTex.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly IHorario _horarioRepository;

        /// <summary>
        /// Constructor que inyecta la interfaz del repositorio de horarios.
        /// </summary>
        /// <param name="horarioRepository">Instancia del repositorio IHorario.</param>
        public HorariosController(IHorario horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        /// <summary>
        /// Obtiene todos los horarios.
        /// </summary>
        [HttpGet("GetHorarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHorarios()
        {
            try
            {
                var horarios = await _horarioRepository.GetAllAsync();
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un horario por su ID.
        /// </summary>
        /// <param name="id">Identificador del horario.</param>
        [HttpGet("GetHorario/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHorario(int id)
        {
            try
            {
                var horario = await _horarioRepository.GetByIdAsync(id);
                if (horario == null)
                    return NotFound($"No se encontró un horario con el ID {id}");

                return Ok(horario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }




        [HttpGet("GetHorariosPorFecha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHorariosPorFecha([FromQuery] DateTime fecha)
        {
            try
            {
                var horarios = await _horarioRepository.GetByFechaAsync(fecha);
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea un nuevo horario.
        /// </summary>
        /// <param name="horario">Objeto Horario a crear.</param>
        [HttpPost("PostHorario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostHorario([FromBody] Horario horario)
        {
            if (horario == null)
                return BadRequest("El objeto horario no puede ser nulo.");

            // Validación mínima para campos requeridos
            if (horario.EmpleadoId <= 0 || string.IsNullOrWhiteSpace(horario.Tipo))
                return BadRequest("EmpleadoId y Tipo son obligatorios.");

            try
            {
                // IMPORTANTE: Ignorar navegación 'Usuario' si viene en el body
                horario.Usuario = null;

                var creado = await _horarioRepository.CreateAsync(horario);
                return CreatedAtAction(nameof(GetHorario), new { id = creado.HorarioId }, creado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al crear el horario: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un horario por su ID.
        /// </summary>
        /// <param name="id">Identificador del horario a eliminar.</param>
        [HttpDelete("DeleteHorario/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            try
            {
                var horario = await _horarioRepository.GetByIdAsync(id);
                if (horario == null)
                    return NotFound($"No se encontró un horario con el ID {id}");

                await _horarioRepository.DeleteAsync(id);
                return Ok($"Horario con ID {id} eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el horario: {ex.Message}");
            }
        }
    }
}
