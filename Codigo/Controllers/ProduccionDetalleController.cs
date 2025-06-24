using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class ProduccionDetalleController : ControllerBase
    {
        private readonly IProduccionDetalle _produccionDetalleService;

        public ProduccionDetalleController(IProduccionDetalle produccionDetalleService)
        {
            _produccionDetalleService = produccionDetalleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduccionDetalles()
        {
            try
            {
                var detalles = await _produccionDetalleService.GetAllAsync();
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener los detalles de producción.", error = ex.Message });
            }
        }

        [HttpPost("crear-con-calculo")]
        public async Task<IActionResult> CrearDetalleConValorTotal([FromBody] ProduccionDetalle detalle)
        {
            if (detalle == null || detalle.OperacionId == 0 || detalle.Cantidad <= 0)
                return BadRequest(new { mensaje = "Datos inválidos para el detalle de producción." });

            try
            {
                var creado = await _produccionDetalleService.CrearConCalculoAsync(detalle);

                if (creado == null)
                    return BadRequest(new { mensaje = "No se pudo crear el detalle. Verifique los datos enviados." });

                return Ok(new
                {
                    mensaje = "Detalle de producción creado correctamente.",
                    detalle = new
                    {
                        creado.Id,
                        creado.OperacionId,
                        creado.Cantidad,
                        creado.ValorTotal,
                        creado.ProduccionId
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear el detalle de producción.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduccionDetalle(int id)
        {
            try
            {
                var eliminado = await _produccionDetalleService.DeleteAsync(id);

                if (!eliminado)
                    return NotFound(new { mensaje = $"Detalle con ID {id} no encontrado." });

                return Ok(new { mensaje = $"Detalle con ID {id} eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar el detalle de producción.", error = ex.Message });
            }
        }

        [HttpGet("por-usuario/{usuarioId}")]
        public async Task<IActionResult> GetDetallesPorUsuario(int usuarioId)
        {
            try
            {
                var detalles = await _produccionDetalleService.GetDetallesPorUsuarioIdAsync(usuarioId);
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener los detalles por usuario.", error = ex.Message });
            }
        }

    }
}
