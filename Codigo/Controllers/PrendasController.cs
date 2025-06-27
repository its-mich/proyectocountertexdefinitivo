using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar las prendas del sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PrendasController : ControllerBase
    {
        private readonly IPrenda _prendaRepository;

        /// <summary>
        /// Constructor que inyecta el repositorio de prendas.
        /// </summary>
        /// <param name="prendaRepository">Repositorio de prendas.</param>
        public PrendasController(IPrenda prendaRepository)
        {
            _prendaRepository = prendaRepository;
        }

        /// <summary>
        /// Obtiene todas las prendas registradas.
        /// </summary>
        /// <returns>Lista de prendas.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prenda>>> GetPrendas()
        {
            try
            {
                var prendas = await _prendaRepository.GetAllAsync();
                return Ok(prendas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las prendas: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene una prenda por su ID.
        /// </summary>
        /// <param name="id">ID de la prenda.</param>
        /// <returns>La prenda correspondiente o un error si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Prenda>> GetPrenda(int id)
        {
            try
            {
                var prenda = await _prendaRepository.GetByIdAsync(id);
                if (prenda == null)
                    return NotFound($"No se encontró la prenda con ID {id}");

                return Ok(prenda);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la prenda: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea una nueva prenda.
        /// </summary>
        /// <param name="prenda">Objeto prenda a crear.</param>
        /// <returns>La prenda creada con su ID.</returns>
        [HttpPost]
        public async Task<ActionResult<Prenda>> PostPrenda(Prenda prenda)
        {
            try
            {
                var nuevaPrenda = await _prendaRepository.CreateAsync(prenda);
                return CreatedAtAction(nameof(GetPrenda), new { id = nuevaPrenda.Id }, nuevaPrenda);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la prenda: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza una prenda existente.
        /// </summary>
        /// <param name="id">ID de la prenda a actualizar.</param>
        /// <param name="prenda">Datos de la prenda actualizada.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrenda(int id, Prenda prenda)
        {
            if (id != prenda.Id)
                return BadRequest("El ID de la URL no coincide con el ID de la prenda.");

            try
            {
                var existente = await _prendaRepository.GetByIdAsync(id);
                if (existente == null)
                    return NotFound($"No se encontró la prenda con ID {id}");

                await _prendaRepository.UpdateAsync(prenda);
                return Ok($"Prenda con ID {id} actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la prenda: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina una prenda por su ID.
        /// </summary>
        /// <param name="id">ID de la prenda a eliminar.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrenda(int id)
        {
            try
            {
                var existente = await _prendaRepository.GetByIdAsync(id);
                if (existente == null)
                    return NotFound($"No se encontró la prenda con ID {id}");

                await _prendaRepository.DeleteAsync(id);
                return Ok($"Prenda con ID {id} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la prenda: {ex.Message}");
            }
        }
    }
}
