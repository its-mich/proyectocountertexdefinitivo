using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones del sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OperacionController : ControllerBase
    {
        private readonly IOperacion _operacionRepository;

        /// <summary>
        /// Constructor con inyección del repositorio de operaciones.
        /// </summary>
        public OperacionController(IOperacion operacionRepository)
        {
            _operacionRepository = operacionRepository;
        }

        /// <summary>
        /// Obtiene todas las operaciones registradas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operacion>>> GetOperacion()
        {
            try
            {
                var operaciones = await _operacionRepository.GetAllAsync();
                return Ok(operaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las operaciones: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene una operación por su ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Operacion>> GetOperacion(int id)
        {
            try
            {
                var operacion = await _operacionRepository.GetByIdAsync(id);
                if (operacion == null)
                    return NotFound($"No se encontró la operación con ID {id}.");

                return Ok(operacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la operación: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea una nueva operación.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostOperacion(Operacion operacion)
        {
            try
            {
                var operacionCreada = await _operacionRepository.CreateAsync(operacion);
                return CreatedAtAction(nameof(GetOperacion), new { id = operacionCreada.Id }, operacionCreada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la operación: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza los datos de una operación existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperacion(int id, Operacion operacion)
        {
            if (id != operacion.Id)
                return BadRequest("El ID de la operación no coincide con el proporcionado en la ruta.");

            try
            {
                var existente = await _operacionRepository.GetByIdAsync(id);
                if (existente == null)
                    return NotFound($"No se encontró la operación con ID {id}.");

                existente.Nombre = operacion.Nombre;
                existente.ValorUnitario = operacion.ValorUnitario;

                await _operacionRepository.UpdateAsync(existente);
                return Ok("Operación actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la operación: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina una operación por su ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperacion(int id)
        {
            try
            {
                var operacion = await _operacionRepository.GetByIdAsync(id);
                if (operacion == null)
                    return NotFound($"No se encontró la operación con ID {id}.");

                await _operacionRepository.DeleteAsync(id);
                return Ok($"Operación con ID {id} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la operación: {ex.Message}");
            }
        }
    }
}
