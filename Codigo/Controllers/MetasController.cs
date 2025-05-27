using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar las metas de producción.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MetasController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto de base de datos.</param>
        public MetasController(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las metas registradas.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="Meta"/>.</returns>
        /// <response code="200">Lista obtenida exitosamente.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meta>>> GetMetas()
        {
            return await _context.Metas.ToListAsync();
        }

        /// <summary>
        /// Obtiene una meta específica por su ID.
        /// </summary>
        /// <param name="id">ID de la meta a consultar.</param>
        /// <returns>El objeto <see cref="Meta"/> encontrado o <see cref="NotFound"/> si no existe.</returns>
        /// <response code="200">Meta encontrada exitosamente.</response>
        /// <response code="404">No se encontró la meta solicitada.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Meta>> GetMeta(int id)
        {
            var meta = await _context.Metas.FindAsync(id);

            if (meta == null)
            {
                return NotFound();
            }

            return meta;
        }

        /// <summary>
        /// Crea una nueva meta.
        /// </summary>
        /// <param name="meta">Objeto <see cref="Meta"/> con los datos a registrar.</param>
        /// <returns>La meta creada junto con su URI de acceso.</returns>
        /// <response code="201">Meta creada correctamente.</response>
        [HttpPost]
        public async Task<ActionResult<Meta>> PostMeta(Meta meta)
        {
            _context.Metas.Add(meta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeta", new { id = meta.Id }, meta);
        }

        /// <summary>
        /// Elimina una meta específica por su ID.
        /// </summary>
        /// <param name="id">ID de la meta a eliminar.</param>
        /// <returns><see cref="NoContent"/> si se elimina correctamente, o <see cref="NotFound"/> si no existe.</returns>
        /// <response code="204">Meta eliminada correctamente.</response>
        /// <response code="404">Meta no encontrada.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeta(int id)
        {
            var meta = await _context.Metas.FindAsync(id);
            if (meta == null)
            {
                return NotFound();
            }

            _context.Metas.Remove(meta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
