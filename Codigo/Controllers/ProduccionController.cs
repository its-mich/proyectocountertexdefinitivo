using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para la gestión de registros de producción.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class ProduccionController : ControllerBase
    {
        private readonly IProduccion _produccionRepo;

        /// <summary>
        /// Constructor que inyecta el repositorio de producción.
        /// </summary>
        /// <param name="produccionRepo">Repositorio de producción.</param>
        public ProduccionController(IProduccion produccionRepo)
        {
            _produccionRepo = produccionRepo;
        }

        /// <summary>
        /// Obtiene todas las producciones registradas.
        /// </summary>
        /// <returns>Lista de producciones.</returns>
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

        /// <summary>
        /// Obtiene una producción por su ID.
        /// </summary>
        /// <param name="id">ID de la producción.</param>
        /// <returns>Producción encontrada o mensaje de error.</returns>
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

        /// <summary>
        /// Crea una nueva producción con sus detalles.
        /// </summary>
        /// <param name="produccion">Objeto Produccion con detalles incluidos.</param>
        /// <returns>Producción creada o mensaje de error.</returns>
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

        /// <summary>
        /// Elimina una producción por su ID.
        /// </summary>
        /// <param name="id">ID de la producción.</param>
        /// <returns>Resultado de la eliminación.</returns>
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

        /// <summary>
        /// Obtiene el resumen mensual de producción por año, mes y filtros opcionales.
        /// </summary>
        /// <param name="anio">Año del resumen.</param>
        /// <param name="mes">Mes del resumen.</param>
        /// <param name="usuarioId">ID del usuario (opcional).</param>
        /// <param name="tipoPrenda">Tipo de prenda (opcional).</param>
        /// <returns>Resumen mensual de producción.</returns>
        [HttpGet("GetResumenMensual")]
        public async Task<IActionResult> GetResumenMensual(int anio, int mes, int? usuarioId = null, string tipoPrenda = null)
        {
            try
            {
                var resumen = await _produccionRepo.ObtenerResumenMensual(anio, mes, usuarioId, tipoPrenda);
                var resumenList = resumen as IEnumerable<object>;
                if (resumenList == null || !resumenList.Any())
                    return NotFound("No se encontraron datos para el resumen mensual.");

                return Ok(resumen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el resumen mensual: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene las producciones realizadas por un usuario específico.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <returns>Lista de producciones realizadas por el usuario.</returns>
        [HttpGet("empleado/{usuarioId}")]
        public async Task<IActionResult> GetProduccionesPorUsuario(int usuarioId)
        {
            try
            {
                var producciones = await _produccionRepo.GetProduccionesPorUsuarioIdAsync(usuarioId);

                if (producciones == null || !producciones.Any())
                    return NotFound("No hay registros de producción para este usuario.");

                var resultado = producciones.Select(p => new ProduccionApiDto
                {
                    Id = p.Id,
                    Fecha = p.Fecha,
                    PrendaId = p.PrendaId,
                    PrendaNombre = p.Prenda?.Nombre ?? "N/A",
                    TotalValor = p.TotalValor,
                    ProduccionDetalles = p.ProduccionDetalles.Select(d => new ProduccionDetalleDto
                    {
                        OperacionId = d.OperacionId,
                        Cantidad = d.Cantidad
                    }).ToList()
                });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener producciones por usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene la lista de tipos de prenda únicos registrados.
        /// </summary>
        /// <returns>Lista de tipos de prenda.</returns>
        [HttpGet("GetTiposPrenda")]
        public async Task<IActionResult> GetTiposPrenda()
        {
            try
            {
                var tipos = await _produccionRepo.ObtenerTiposPrendaAsync();
                return Ok(tipos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener tipos de prenda: {ex.Message}");
            }
        }
    }
}
