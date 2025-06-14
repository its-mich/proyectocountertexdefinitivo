using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class ProduccionController : ControllerBase
    {
        private readonly IProduccion _produccionRepo;

        public ProduccionController(IProduccion produccionRepo)
        {
            _produccionRepo = produccionRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducciones()
        {
            try
            {
                var producciones = await _produccionRepo.GetAllAsync();
                return Ok(producciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener producciones: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduccion(int id)
        {
            try
            {
                var produccion = await _produccionRepo.GetByIdAsync(id);
                if (produccion == null)
                    return NotFound($"Producción con ID {id} no encontrada.");

                return Ok(produccion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la producción: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearProduccion([FromBody] Produccion produccion)
        {
            if (produccion == null || produccion.ProduccionDetalles == null || !produccion.ProduccionDetalles.Any())
                return BadRequest("Producción inválida. Asegúrese de enviar detalles.");

            try
            {
                produccion.TotalValor = produccion.ProduccionDetalles.Sum(d => d.ValorTotal ?? 0);
                var creada = await _produccionRepo.CreateAsync(produccion);
                return CreatedAtAction(nameof(GetProduccion), new { id = creada.Id }, creada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la producción: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProduccion(int id)
        {
            try
            {
                var eliminada = await _produccionRepo.DeleteAsync(id);
                if (!eliminada)
                    return NotFound($"Producción con ID {id} no encontrada.");

                return Ok($"Producción con ID {id} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la producción: {ex.Message}");
            }
        }

        [HttpGet("GetResumenMensual")]
        public async Task<IActionResult> GetResumenMensual(int anio, int mes)
        {
            try
            {
                var resumen = await _produccionRepo.ObtenerResumenMensual(anio, mes);
                if (resumen == null)
                    return NotFound("No se encontraron datos para el resumen mensual.");

                return Ok(resumen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el resumen mensual: {ex.Message}");
            }
        }
    }
}
