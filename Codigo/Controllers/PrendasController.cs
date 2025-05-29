using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para la gestión de prendas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PrendasController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de la base de datos.</param>
        public PrendasController(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de todas las prendas.
        /// </summary>
        /// <returns>Lista de prendas.</returns>
        /// <response code="200">Prendas obtenidas correctamente.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prenda>>> GetPrendas()
        {
            return await _context.Prendas.ToListAsync();
        }

        /// <summary>
        /// Obtiene una prenda por su ID.
        /// </summary>
        /// <param name="id">ID de la prenda.</param>
        /// <returns>La prenda solicitada.</returns>
        /// <response code="200">Prenda encontrada.</response>
        /// <response code="404">No se encontró la prenda.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Prenda>> GetPrenda(int id)
        {
            var prenda = await _context.Prendas.FindAsync(id);

            if (prenda == null)
            {
                return NotFound();
            }

            return prenda;
        }

        /// <summary>
        /// Actualiza una prenda existente.
        /// </summary>
        /// <param name="id">ID de la prenda.</param>
        /// <param name="prenda">Objeto prenda con datos actualizados.</param>
        /// <returns>NoContent si se actualiza correctamente.</returns>
        /// <response code="204">Actualización exitosa.</response>
        /// <response code="400">ID de la URL no coincide con el del objeto.</response>
        /// <response code="404">Prenda no encontrada.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrenda(int id, Prenda prenda)
        {
            if (id != prenda.Id)
            {
                return BadRequest();
            }

            _context.Entry(prenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Crea una nueva prenda.
        /// </summary>
        /// <param name="prenda">Objeto prenda a crear.</param>
        /// <returns>La prenda creada.</returns>
        /// <response code="201">Prenda creada correctamente.</response>
        [HttpPost]
        public async Task<ActionResult<Prenda>> PostPrenda(Prenda prenda)
        {
            _context.Prendas.Add(prenda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrenda", new { id = prenda.Id }, prenda);
        }

        /// <summary>
        /// Elimina una prenda por su ID.
        /// </summary>
        /// <param name="id">ID de la prenda a eliminar.</param>
        /// <returns>NoContent si se elimina correctamente.</returns>
        /// <response code="204">Prenda eliminada correctamente.</response>
        /// <response code="404">Prenda no encontrada.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrenda(int id)
        {
            var prenda = await _context.Prendas.FindAsync(id);
            if (prenda == null)
            {
                return NotFound();
            }

            _context.Prendas.Remove(prenda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica si una prenda existe en la base de datos.
        /// </summary>
        /// <param name="id">ID de la prenda.</param>
        /// <returns>true si existe, false si no.</returns>
        private bool PrendaExists(int id)
        {
            return _context.Prendas.Any(e => e.Id == id);
        }
    }
}
