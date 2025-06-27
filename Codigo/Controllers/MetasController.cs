using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador API para gestionar las metas de producción.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MetasController : ControllerBase
    {
        private readonly IMeta _metaRepository;

        /// <summary>
        /// Constructor del controlador que inyecta el repositorio de metas.
        /// </summary>
        /// <param name="metaRepository">Repositorio de metas.</param>
        public MetasController(IMeta metaRepository)
        {
            _metaRepository = metaRepository;
        }

        /// <summary>
        /// Obtiene todas las metas registradas.
        /// </summary>
        /// <returns>Una lista de metas.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meta>>> GetMetas()
        {
            try
            {
                var metas = await _metaRepository.GetAllAsync();
                return Ok(metas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las metas: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene una meta específica por su ID.
        /// </summary>
        /// <param name="id">ID de la meta.</param>
        /// <returns>Una meta si se encuentra; 404 si no existe.</returns>
        [HttpGet("GetMeta/{id}")]
        public async Task<ActionResult<Meta>> GetMeta(int id)
        {
            try
            {
                var meta = await _metaRepository.GetByIdAsync(id);
                if (meta == null)
                    return NotFound();

                return Ok(meta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la meta: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea una nueva meta.
        /// </summary>
        /// <param name="meta">Objeto meta a crear.</param>
        /// <returns>La meta creada con su ID.</returns>
        [HttpPost("PostMetas")]
        public async Task<ActionResult<Meta>> PostMeta(Meta meta)
        {
            try
            {
                var nuevaMeta = await _metaRepository.CreateAsync(meta);
                return CreatedAtAction(nameof(GetMeta), new { id = nuevaMeta.Id }, nuevaMeta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la meta: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina una meta por su ID.
        /// </summary>
        /// <param name="id">ID de la meta a eliminar.</param>
        /// <returns>Mensaje indicando si fue eliminada o no encontrada.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeta(int id)
        {
            try
            {
                var meta = await _metaRepository.GetByIdAsync(id);
                if (meta == null)
                {
                    return NotFound(new { mensaje = "Meta no encontrada." });
                }

                await _metaRepository.DeleteAsync(id);
                return Ok(new { mensaje = "Meta eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al eliminar la meta: {ex.Message}" });
            }
        }
    }
}
