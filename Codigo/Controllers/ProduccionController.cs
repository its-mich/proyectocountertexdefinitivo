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

        /// <summary>
        /// Obtiene todas las producciones registradas por un empleado específico.
        /// </summary>
        /// <param name="id">ID del empleado (usuario logueado)</param>
        /// <returns>Lista de producciones asociadas a ese empleado</returns>
        [HttpGet("empleado/{id}")]
        public async Task<ActionResult<IEnumerable<Produccion>>> GetProduccionPorEmpleado(int id)
        {
            // Llama al método del repositorio que filtra por el usuarioId (empleado)
            var producciones = await _produccionRepo.ObtenerProduccionesPorEmpleadoAsync(id);

            // Retorna las producciones en formato JSON con estado 200 OK
            return Ok(producciones);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProduccion(int id, [FromBody] Produccion produccion)
        {
            if (id != produccion.Id)
                return BadRequest("El ID proporcionado no coincide con la producción.");

            try
            {
                var actualizado = await _produccionRepo.ActualizarProduccionConDetallesAsync(produccion);
                if (!actualizado)
                    return NotFound($"Producción con ID {id} no encontrada.");

                return Ok(new { mensaje = "Producción actualizada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la producción: {ex.Message}");
            }
        }



    }
}
