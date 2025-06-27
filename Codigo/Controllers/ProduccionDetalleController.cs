using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar los detalles de producción.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class ProduccionDetalleController : ControllerBase
    {
        private readonly IProduccionDetalle _produccionDetalleService;

        /// <summary>
        /// Constructor que inyecta el servicio de detalles de producción.
        /// </summary>
        /// <param name="produccionDetalleService">Interfaz de servicio de ProducciónDetalle.</param>
        public ProduccionDetalleController(IProduccionDetalle produccionDetalleService)
        {
            _produccionDetalleService = produccionDetalleService;
        }

        /// <summary>
        /// Obtiene todos los detalles de producción registrados.
        /// </summary>
        /// <returns>Lista de detalles de producción.</returns>
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

        /// <summary>
        /// Crea un nuevo detalle de producción y calcula su valor total automáticamente.
        /// </summary>
        /// <param name="detalle">Objeto detalle de producción.</param>
        /// <returns>Detalle creado con su valor total calculado.</returns>
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

        /// <summary>
        /// Elimina un detalle de producción por su ID.
        /// </summary>
        /// <param name="id">ID del detalle de producción.</param>
        /// <returns>Resultado de la operación.</returns>
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

        /// <summary>
        /// Obtiene los detalles de producción filtrados por ID de usuario.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <returns>Lista de detalles de producción del usuario.</returns>
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
