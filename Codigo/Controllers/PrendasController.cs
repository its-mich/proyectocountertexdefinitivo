using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class PrendasController : ControllerBase
    {
        private readonly IPrenda _prendaRepository;

        public PrendasController(IPrenda prendaRepository)
        {
            _prendaRepository = prendaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prenda>>> GetPrendas()
        {
            try
            {
                var prendas = await _prendaRepository.GetAllAsync();
                return Ok(prendas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las prendas: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prenda>> GetPrenda(int id)
        {
            try
            {
                var prenda = await _prendaRepository.GetByIdAsync(id);
                if (prenda == null)
                    return NotFound($"No se encontró la prenda con ID {id}");

                return Ok(prenda);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la prenda: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Prenda>> PostPrenda(Prenda prenda)
        {
            try
            {
                var nuevaPrenda = await _prendaRepository.CreateAsync(prenda);
                return CreatedAtAction(nameof(GetPrenda), new { id = nuevaPrenda.Id }, nuevaPrenda);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la prenda: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrenda(int id, Prenda prenda)
        {
            if (id != prenda.Id)
                return BadRequest("El ID de la URL no coincide con el ID de la prenda.");

            try
            {
                var existente = await _prendaRepository.GetByIdAsync(id);
                if (existente == null)
                    return NotFound($"No se encontró la prenda con ID {id}");

                await _prendaRepository.UpdateAsync(prenda);
                return Ok($"Prenda con ID {id} actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la prenda: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrenda(int id)
        {
            try
            {
                var existente = await _prendaRepository.GetByIdAsync(id);
                if (existente == null)
                    return NotFound($"No se encontró la prenda con ID {id}");

                await _prendaRepository.DeleteAsync(id);
                return Ok($"Prenda con ID {id} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la prenda: {ex.Message}");
            }
        }
    }
}
