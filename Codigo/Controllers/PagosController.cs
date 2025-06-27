using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para la gestión de pagos quincenales y pagos a proveedores.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagoRepository _repo;
        private readonly IPagoProveedorRepository _repoProveedor;

        /// <summary>
        /// Constructor del controlador de pagos.
        /// </summary>
        /// <param name="repo">Repositorio para pagos a empleados.</param>
        /// <param name="repoProveedor">Repositorio para pagos a proveedores.</param>
        public PagosController(IPagoRepository repo, IPagoProveedorRepository repoProveedor)
        {
            _repo = repo;
            _repoProveedor = repoProveedor;
        }

        /// <summary>
        /// Genera pagos quincenales para empleados en un rango de fechas.
        /// </summary>
        /// <param name="fechas">Rango de fechas con FechaInicio y FechaFin.</param>
        /// <returns>Mensaje de éxito si se generan los pagos.</returns>
        [HttpPost("generar")]
        public async Task<IActionResult> GenerarPagoQuincenal([FromBody] RangoFechasDto fechas)
        {
            await _repo.GenerarPagoQuincenalAsync(fechas.FechaInicio, fechas.FechaFin);
            return Ok(new { mensaje = "Pagos generados correctamente" });
        }

        /// <summary>
        /// Obtiene todos los pagos registrados.
        /// </summary>
        /// <returns>Lista de pagos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetPagos()
        {
            var pagos = await _repo.ObtenerPagosAsync();
            return Ok(pagos);
        }

        /// <summary>
        /// Obtiene los pagos realizados a un usuario específico.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <returns>Lista de pagos asociados al usuario.</returns>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Pago>>> ObtenerPagosPorUsuario(int usuarioId)
        {
            var pagos = await _repo.ObtenerPagosPorUsuarioAsync(usuarioId);
            return Ok(pagos);
        }

        /// <summary>
        /// Registra un nuevo pago a un proveedor.
        /// </summary>
        /// <param name="dto">Datos del pago al proveedor.</param>
        /// <returns>Mensaje indicando el resultado del registro.</returns>
        [HttpPost("proveedor")]
        public async Task<IActionResult> RegistrarPagoProveedor([FromBody] PagoProveedorDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pago = new PagoProveedor
            {
                ProveedorId = dto.ProveedorId,
                CantidadPrendas = dto.CantidadPrendas,
                PrecioUnitario = dto.PrecioUnitario,
                TotalPagado = dto.CantidadPrendas * dto.PrecioUnitario,
                Observaciones = dto.Observaciones,
                FechaRegistro = DateTime.Now
            };

            await _repoProveedor.AgregarPagoProveedorAsync(pago);

            return Ok(new { message = "Pago registrado exitosamente." });
        }
    }
}
