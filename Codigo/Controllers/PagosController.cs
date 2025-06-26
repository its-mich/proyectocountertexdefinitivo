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

        public PagosController(IPagoRepository repo)
        {
            _repo = repo;
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
    }
}
