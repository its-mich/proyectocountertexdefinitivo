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
                return BadRequest(new { mensaje = "Producción inválida. Asegúrese de enviar al menos un detalle." });

            try
            {
                var creada = await _produccionRepo.CrearProduccionConDetallesAsync(produccion);
                if (creada == null)
                    return BadRequest(new { mensaje = "Ocurrió un error al procesar la producción. Verifique los datos enviados." });

                return Ok(new
                {
                    mensaje = "Producción creada exitosamente.",
                    produccion = new
                    {
                        creada.Id,
                        creada.Fecha,
                        creada.TotalValor,
                        creada.UsuarioId,
                        creada.PrendaId,
                        Detalles = creada.ProduccionDetalles.Select(d => new
                        {
                            d.Id,
                            d.Cantidad,
                            d.OperacionId,
                            d.ValorTotal
                        })
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error inesperado al crear la producción.", detalle = ex.Message });
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

        [HttpGet("empleado/{usuarioId}")]
        public async Task<IActionResult> GetProduccionesPorUsuario(int usuarioId)
        {
            try
            {
                var producciones = await _produccionRepo.GetProduccionesPorUsuarioIdAsync(usuarioId);

                if (producciones == null || !producciones.Any())
                    return NotFound("No hay registros de producción para este usuario.");

                return Ok(producciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener producciones por usuario: {ex.Message}");
            }
        }


        /// <summary>
        /// Calcula el total ganado por un usuario en una quincena (rango de fechas).
        /// </summary>
        /// <param name="usuarioId">ID del usuario</param>
        /// <param name="fechaInicio">Fecha de inicio</param>
        /// <param name="fechaFin">Fecha de fin</param>
        /// <returns>Total ganado</returns>
        [HttpGet("PagoQuincenal")]
        public async Task<IActionResult> CalcularPagoQuincenal(int usuarioId, int año, int mes, int quincena)
        {
            var pago = await _produccionRepo.CalcularPagoQuincenalAsync(usuarioId, año, mes, quincena);

            DateTime inicio = (quincena == 1)
                ? new DateTime(año, mes, 1)
                : new DateTime(año, mes, 16);

            DateTime fin = (quincena == 1)
                ? new DateTime(año, mes, 15)
                : new DateTime(año, mes, DateTime.DaysInMonth(año, mes));

            return Ok(new
            {
                UsuarioId = usuarioId,
                Año = año,
                Mes = mes,
                Quincena = quincena,
                Rango = $"{inicio:yyyy-MM-dd} a {fin:yyyy-MM-dd}",
                PagoTotalQuincenal = pago
            });
        }

    }
}
