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
        public async Task<ActionResult<IEnumerable<ProduccionDTO>>> GetProduccion()
        {
            var producciones = await _context.Producciones
                .Include(p => p.Usuario)
                .Include(p => p.Prenda)
                .Select(p => new ProduccionDTO
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
        public async Task<ActionResult<ProduccionDTO>> GetProduccion(int id)
        {
            var produccion = await _context.Producciones
                .Include(p => p.Usuario)              
                .Include(p => p.Prenda)               
                .Include(p => p.ProduccionDetalles)
                    .ThenInclude(d => d.Operacion)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produccion == null)
            {
                return NotFound();
            }

            // Ya deberías tener los valores almacenados, pero si quieres recalcular:
            produccion.TotalValor = produccion.ProduccionDetalles.Sum(d => d.ValorTotal ?? 0);

            return Ok(produccion);
        }

        /// <summary>
        /// Crea una nueva producción con sus detalles.
        /// </summary>
        /// <param name="produccion">Objeto producción a crear.</param>
        /// <returns>La producción creada.</returns>
        /// <response code="201">Producción creada correctamente.</response>
        /// <response code="400">Datos inválidos o usuario/prenda no existentes.</response>
        [HttpPost]
        public async Task<IActionResult> CrearProduccion([FromBody] ProduccionCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos de producción vacíos.");

            if (dto.ProduccionDetalles == null || !dto.ProduccionDetalles.Any())
                return BadRequest("Debe incluir al menos un detalle de producción.");

            // Validar que Usuario y Prenda existen
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == dto.UsuarioId);
            if (!usuarioExiste)
                return BadRequest($"Usuario con Id {dto.UsuarioId} no existe.");

            var prendaExiste = await _context.Prendas.AnyAsync(p => p.Id == dto.PrendaId);
            if (!prendaExiste)
                return BadRequest($"Prenda con Id {dto.PrendaId} no existe.");

            var produccion = new Produccion
            {
                Fecha = dto.Fecha,
                UsuarioId = dto.UsuarioId,
                PrendaId = dto.PrendaId,
                ProduccionDetalles = new List<ProduccionDetalle>()
            };

            foreach (var detalleDto in dto.ProduccionDetalles)
            {
                var operacion = await _context.Operaciones.FindAsync(detalleDto.OperacionId);
                if (operacion == null)
                    return BadRequest($"Operación con Id {detalleDto.OperacionId} no existe.");

                if (operacion.ValorUnitario == null)
                    return BadRequest($"La operación con Id {detalleDto.OperacionId} no tiene valor unitario asignado.");

                // Calculamos el valor total para evitar confiar en lo que venga del cliente
                decimal valorTotalCalculado = operacion.ValorUnitario.Value * detalleDto.Cantidad;

                var detalle = new ProduccionDetalle
                {
                    OperacionId = detalleDto.OperacionId,
                    Cantidad = detalleDto.Cantidad,
                    ValorTotal = valorTotalCalculado
                };

                produccion.ProduccionDetalles.Add(detalle);
            }
            // Calcular el total acumulado de la producción
            produccion.TotalValor = produccion.ProduccionDetalles.Sum(d => d.ValorTotal ?? 0);

            _context.Producciones.Add(produccion);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar la producción: {ex.Message}");
            }

            return Ok(produccion);
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
            var detalles = await _context.ProduccionDetalles
                                         .Where(d => d.ProduccionId == id)
                                         .ToListAsync();

            _context.ProduccionDetalles.RemoveRange(detalles);
            // Eliminar producción
            // Ahora sí, eliminar la producción
            _context.Producciones.Remove(produccion);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetResumenMensual")]
        public async Task<IActionResult> GetResumenMensual(int anio, int mes, int? usuarioId = null, string tipoPrenda = null)
        {
            var resumen = await _context.ProduccionDetalles
                .Where(d => d.Produccion.Fecha.Year == anio && d.Produccion.Fecha.Month == mes)
                .Where(d => !usuarioId.HasValue || d.Produccion.UsuarioId == usuarioId)
                .Where(d => string.IsNullOrEmpty(tipoPrenda) || d.Produccion.Prenda.Nombre.Contains(tipoPrenda))
                .GroupBy(d => d.Produccion.Prenda.Nombre)
                .Select(g => new
                {
                    prenda = g.Key,
                    total = g.Sum(x => x.Cantidad)
                })
                .ToListAsync();

            return Ok(resumen);
        }
    }
}
