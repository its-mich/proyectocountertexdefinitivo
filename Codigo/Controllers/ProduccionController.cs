using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar las producciones.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProduccionController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Contexto de base de datos.</param>
        public ProduccionController(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de producciones con detalles resumidos.
        /// </summary>
        /// <returns>Lista de producciones con usuario, prenda y total de cantidad producida.</returns>
        /// <response code="200">Lista obtenida correctamente.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProduccionDto>>> GetProduccion()
        {
            var producciones = await _context.Producciones
                .Include(p => p.Usuario)
                .Include(p => p.Prenda)
                .Select(p => new ProduccionDto
                {
                    Id = p.Id,
                    Fecha = p.Fecha,
                    Usuario = p.Usuario.Nombre,
                    Prenda = p.Prenda.Nombre,
                    Total = p.ProduccionDetalles.Sum(d => d.Cantidad)
                })
                .ToListAsync();

            return Ok(producciones);
        }

        /// <summary>
        /// Obtiene una producción específica por su ID.
        /// </summary>
        /// <param name="id">ID de la producción.</param>
        /// <returns>La producción solicitada.</returns>
        /// <response code="200">Producción encontrada.</response>
        /// <response code="404">Producción no encontrada.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Produccion>> GetProduccion(int id)
        {
            var produccion = await _context.Producciones.FindAsync(id);

            if (produccion == null)
            {
                return NotFound();
            }

            return produccion;
        }

        /// <summary>
        /// Crea una nueva producción con sus detalles.
        /// </summary>
        /// <param name="produccion">Objeto producción a crear.</param>
        /// <returns>La producción creada.</returns>
        /// <response code="201">Producción creada correctamente.</response>
        /// <response code="400">Datos inválidos o usuario/prenda no existentes.</response>
        [HttpPost]
        public async Task<ActionResult<Produccion>> PostProduccion(Produccion produccion)
        {
            // Validación de campos requeridos
            if (produccion.UsuarioId <= 0 || produccion.PrendaId <= 0)
            {
                return BadRequest("El usuario y la prenda deben ser válidos.");
            }

            // Verificar existencia de usuario y prenda
            var usuario = await _context.Usuarios.FindAsync(produccion.UsuarioId);
            var prenda = await _context.Prendas.FindAsync(produccion.PrendaId);

            if (usuario == null || prenda == null)
            {
                return BadRequest("El usuario o la prenda no existen.");
            }

            // Añadir la producción
            _context.Producciones.Add(produccion);
            await _context.SaveChangesAsync();

            // Asignar ProduccionId a los detalles y guardarlos
            foreach (var detalle in produccion.ProduccionDetalles)
            {
                detalle.ProduccionId = produccion.Id;
                _context.ProduccionDetalle.Add(detalle);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduccion), new { id = produccion.Id }, produccion);
        }

        /// <summary>
        /// Elimina una producción y sus detalles relacionados.
        /// </summary>
        /// <param name="id">ID de la producción a eliminar.</param>
        /// <returns>NoContent si se elimina correctamente.</returns>
        /// <response code="204">Producción eliminada.</response>
        /// <response code="404">Producción no encontrada.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduccion(int id)
        {
            var produccion = await _context.Producciones.FindAsync(id);
            if (produccion == null)
            {
                return NotFound();
            }

            // Eliminar detalles relacionados
            var detalles = await _context.ProduccionDetalle
                                         .Where(d => d.ProduccionId == id)
                                         .ToListAsync();

            _context.ProduccionDetalle.RemoveRange(detalles);

            // Eliminar producción
            _context.Producciones.Remove(produccion);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
