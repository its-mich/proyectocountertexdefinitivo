using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar los detalles de producción.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProduccionDetalleController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de la base de datos.</param>
        public ProduccionDetalleController(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los detalles de producción.
        /// </summary>
        /// <returns>Lista de detalles de producción.</returns>
        /// <response code="200">Detalles obtenidos correctamente.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProduccionDetalle>>> GetProduccionDetalles()
        {
            return await _context.ProduccionDetalle.ToListAsync();
        }

        /// <summary>
        /// Crea un nuevo detalle de producción sin cálculo automático.
        /// </summary>
        /// <param name="produccionDetalle">Detalle de producción a crear.</param>
        /// <returns>Detalle creado.</returns>
        /// <response code="201">Detalle creado correctamente.</response>
        [HttpPost]
        public async Task<ActionResult<ProduccionDetalle>> PostProduccionDetalle(ProduccionDetalle produccionDetalle)
        {
            _context.ProduccionDetalle.Add(produccionDetalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduccionDetalle", new { id = produccionDetalle.Id }, produccionDetalle);
        }

        /// <summary>
        /// Crea un detalle de producción calculando automáticamente el ValorTotal
        /// multiplicando la cantidad por el ValorUnitario de la operación relacionada.
        /// </summary>
        /// <param name="detalle">Detalle de producción con datos iniciales.</param>
        /// <returns>Detalle creado con ValorTotal calculado.</returns>
        /// <response code="201">Detalle creado correctamente.</response>
        /// <response code="400">Operación no encontrada.</response>
        [HttpPost("crear-con-calculo")]
        public async Task<ActionResult<ProduccionDetalle>> CrearDetalleConValorTotal(ProduccionDetalle detalle)
        {
            var operacion = await _context.Operaciones
                .FirstOrDefaultAsync(o => o.Id == detalle.OperacionId);

            if (operacion == null)
            {
                return BadRequest("Operación no encontrada.");
            }

            detalle.ValorTotal = detalle.Cantidad * operacion.ValorUnitario.GetValueOrDefault();

            _context.ProduccionDetalle.Add(detalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduccionDetalles), new { id = detalle.Id }, detalle);
        }

        /// <summary>
        /// Elimina un detalle de producción por su ID.
        /// </summary>
        /// <param name="id">ID del detalle a eliminar.</param>
        /// <returns>NoContent si se elimina correctamente.</returns>
        /// <response code="204">Detalle eliminado.</response>
        /// <response code="404">Detalle no encontrado.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduccionDetalle(int id)
        {
            var produccionDetalle = await _context.ProduccionDetalle.FindAsync(id);
            if (produccionDetalle == null)
            {
                return NotFound();
            }

            _context.ProduccionDetalle.Remove(produccionDetalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
