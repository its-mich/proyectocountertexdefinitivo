using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagoRepository _repo;
        private readonly IPagoProveedorRepository _repoProveedor;

        public PagosController(IPagoRepository repo, IPagoProveedorRepository repoProveedor)
        {
            _repo = repo;
            _repoProveedor = repoProveedor;
        }

        [HttpPost("generar")]
        public async Task<IActionResult> GenerarPagoQuincenal([FromBody] RangoFechasDto fechas)
        {
            await _repo.GenerarPagoQuincenalAsync(fechas.FechaInicio, fechas.FechaFin);
            return Ok(new { mensaje = "Pagos generados correctamente" });
        }

        [HttpGet]
        public async Task<IActionResult> GetPagos()
        {
            var pagos = await _repo.ObtenerPagosAsync();
            return Ok(pagos);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Pago>>> ObtenerPagosPorUsuario(int usuarioId)
        {
            var pagos = await _repo.ObtenerPagosPorUsuarioAsync(usuarioId);
            return Ok(pagos);
        }

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
