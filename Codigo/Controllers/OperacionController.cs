using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones del sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OperacionController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de base de datos.</param>
        public OperacionController(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las operaciones registradas.
        /// </summary>
        /// <returns>Lista de operaciones.</returns>
        /// <response code="200">Operaciones obtenidas correctamente.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operacion>>> GetOperacion()
        {
            return await _context.Operaciones.ToListAsync();
        }

        /// <summary>
        /// Obtiene una operación por su ID.
        /// </summary>
        /// <param name="id">ID de la operación.</param>
        /// <returns>La operación encontrada o 404 si no existe.</returns>
        /// <response code="200">Operación encontrada.</response>
        /// <response code="404">No se encontró la operación.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Operacion>> GetOperacion(int id)
        {
            var operacion = await _context.Operaciones.FindAsync(id);

            if (operacion == null)
            {
                return NotFound();
            }

            return operacion;
        }

        /// <summary>
        /// Actualiza los datos de una operación existente.
        /// </summary>
        /// <param name="id">ID de la operación a actualizar.</param>
        /// <param name="dto">Datos actualizados de la operación.</param>
        /// <returns>NoContent si se actualiza correctamente.</returns>
        /// <response code="204">Actualización exitosa.</response>
        /// <response code="400">El ID no coincide con el DTO.</response>
        /// <response code="404">Operación no encontrada.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperacion(int id, OperacionUpdateDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var operacion = await _context.Operaciones.FindAsync(id);
            if (operacion == null)
                return NotFound();

            operacion.Nombre = dto.Nombre;
            operacion.ValorUnitario = dto.ValorUnitario;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Crea una nueva operación.
        /// </summary>
        /// <param name="dto">Datos de la operación a crear.</param>
        /// <returns>La operación creada.</returns>
        /// <response code="201">Operación creada exitosamente.</response>
        [HttpPost]
        public async Task<IActionResult> PostOperacion(OperacionCreateDTO dto)
        {
            var operacion = new Operacion
            {
                Nombre = dto.Nombre,
                ValorUnitario = dto.ValorUnitario
            };

            _context.Operaciones.Add(operacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOperacion), new { id = operacion.Id }, operacion);
        }

        /// <summary>
        /// Elimina una operación por su ID.
        /// </summary>
        /// <param name="id">ID de la operación a eliminar.</param>
        /// <returns>NoContent si se elimina correctamente.</returns>
        /// <response code="204">Operación eliminada correctamente.</response>
        /// <response code="404">Operación no encontrada.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperacion(int id)
        {
            var operacion = await _context.Operaciones.FindAsync(id);
            if (operacion == null)
            {
                return NotFound();
            }

            _context.Operaciones.Remove(operacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica si una operación existe por su ID.
        /// </summary>
        /// <param name="id">ID de la operación.</param>
        /// <returns>true si existe; false en caso contrario.</returns>
        private bool OperacionExists(int id)
        {
            return _context.Operaciones.Any(e => e.Id == id);
        }
    }
}
