using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrendasController : ControllerBase
    {
        private readonly IPrenda _prenda;

        public PrendasController(IPrenda prenda)
        {
            _prenda = prenda;
        }

        [HttpGet("GetPrendas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPrendas()
        {
            try
            {
                var response = await _prenda.GetAllAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostPrenda")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostPrenda([FromBody] Prenda prenda)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Datos inválidos.");

                var creada = await _prenda.CreateAsync(prenda);
                return CreatedAtAction(nameof(GetPrendaById), new { id = creada.Id }, creada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPrendaById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPrendaById(int id)
        {
            try
            {
                var prenda = await _prenda.GetByIdAsync(id);
                if (prenda == null)
                    return NotFound("No se encontró la prenda con ese ID.");

                return Ok(prenda);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePrenda/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePrenda(int id, [FromBody] Prenda prenda)
        {
            try
            {
                if (id != prenda.Id)
                    return BadRequest("El ID no coincide con el del objeto.");

                var existente = await _prenda.GetByIdAsync(id);
                if (existente == null)
                    return NotFound("Prenda no encontrada.");

                await _prenda.UpdateAsync(prenda);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePrenda/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePrenda(int id)
        {
            try
            {
                var existente = await _prenda.GetByIdAsync(id);
                if (existente == null)
                    return NotFound("Prenda no encontrada.");

                await _prenda.DeleteAsync(id);
                return Ok("Prenda eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
