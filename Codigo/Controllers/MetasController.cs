using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetasController : ControllerBase
    {
        private readonly IMeta _metaRepository;

        public MetasController(IMeta metaRepository)
        {
            _metaRepository = metaRepository;
        }

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

        [HttpGet("{id}")]
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

        [HttpPost]
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
