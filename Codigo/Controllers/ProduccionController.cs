using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using Microsoft.AspNetCore.Authorization; // Necesario para [AllowAnonymous]

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar las producciones.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken] // <--- ¡AÑADIR ESTO AQUÍ!
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
        [AllowAnonymous]
        public async Task<ActionResult<Produccion>> PostProduccion(Produccion produccion)
        {
            // ... (tu código de validación y guardado actual) ...
            if (produccion.UsuarioId <= 0 || produccion.PrendaId <= 0)
            {
                ModelState.AddModelError(nameof(produccion.UsuarioId), "El usuario es obligatorio.");
                ModelState.AddModelError(nameof(produccion.PrendaId), "La prenda es obligatoria.");
                return BadRequest(ModelState);
            }

            // Verificar existencia de usuario y prenda
            var usuario = await _context.Usuarios.FindAsync(produccion.UsuarioId);
            var prenda = await _context.Prendas.FindAsync(produccion.PrendaId);

            if (usuario == null)
            {
                ModelState.AddModelError(nameof(produccion.UsuarioId), "El usuario seleccionado no existe.");
                return BadRequest(ModelState);
            }
            if (prenda == null)
            {
                ModelState.AddModelError(nameof(produccion.PrendaId), "La prenda seleccionada no existe.");
                return BadRequest(ModelState);
            }

            if (produccion.ProduccionDetalles == null || !produccion.ProduccionDetalles.Any())
            {
                ModelState.AddModelError(nameof(produccion.ProduccionDetalles), "Debe agregar al menos un detalle de producción.");
                return BadRequest(ModelState);
            }

            int detalleIndex = 0;
            foreach (var detalle in produccion.ProduccionDetalles)
            {
                if (detalle.Cantidad <= 0)
                {
                    ModelState.AddModelError($"{nameof(produccion.ProduccionDetalles)}[{detalleIndex}].{nameof(detalle.Cantidad)}", "La cantidad del detalle debe ser mayor que cero.");
                }
                if (detalle.OperacionId <= 0)
                {
                    ModelState.AddModelError($"{nameof(produccion.ProduccionDetalles)}[{detalleIndex}].{nameof(detalle.OperacionId)}", "La operación del detalle es obligatoria.");
                }
                var operacion = await _context.Operaciones.FindAsync(detalle.OperacionId);
                if (operacion == null)
                {
                    ModelState.AddModelError($"{nameof(produccion.ProduccionDetalles)}[{detalleIndex}].{nameof(detalle.OperacionId)}", "La operación seleccionada en el detalle no existe.");
                }
                detalleIndex++;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            // Guardar los detalles de producción
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
            // Eliminar primero los detalles relacionados
            var detalles = await _context.ProduccionDetalle
                                         .Where(d => d.ProduccionId == id)
                                         .ToListAsync();

            _context.ProduccionDetalle.RemoveRange(detalles);
            // Eliminar producción
            // Ahora sí, eliminar la producción
            _context.Producciones.Remove(produccion);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
