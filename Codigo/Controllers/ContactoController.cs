using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar los contactos del sistema.
    /// Permite obtener, crear y eliminar contactos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly IContacto _contactoRepository;

        /// <summary>
        /// Constructor con inyección del repositorio de contactos.
        /// </summary>
        /// <param name="contactoRepository">Repositorio de contactos.</param>
        public ContactoController(IContacto contactoRepository)
        {
            _contactoRepository = contactoRepository;
        }

        /// <summary>
        /// Obtiene todos los contactos registrados.
        /// </summary>
        /// <returns>Lista de contactos.</returns>
        [HttpGet("GetContactos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetContactos()
        {
            try
            {
                var contactos = await _contactoRepository.GetAllAsync();
                return Ok(contactos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener los contactos: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un contacto por su ID.
        /// </summary>
        /// <param name="id">ID del contacto.</param>
        /// <returns>Objeto de contacto.</returns>
        [HttpGet("GetContacto/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetContacto(int id)
        {
            try
            {
                var contacto = await _contactoRepository.GetByIdAsync(id);

                if (contacto == null)
                    return NotFound("Contacto no encontrado.");

                return Ok(contacto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener el contacto: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea un nuevo contacto.
        /// </summary>
        /// <param name="contacto">Datos del contacto a registrar.</param>
        /// <returns>Mensaje de éxito o error.</returns>
        [HttpPost("PostContacto")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostContacto([FromBody] Contacto contacto)
        {
            try
            {
                var nuevoContacto = await _contactoRepository.CreateAsync(contacto);
                return CreatedAtAction(nameof(GetContacto), new { id = nuevoContacto.Id }, nuevoContacto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al crear el contacto: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un contacto por su ID.
        /// </summary>
        /// <param name="id">ID del contacto.</param>
        /// <returns>Mensaje de éxito o error.</returns>
        [HttpDelete("DeleteContacto/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            try
            {
                var existente = await _contactoRepository.GetByIdAsync(id);

                if (existente == null)
                    return NotFound("Contacto no encontrado.");

                await _contactoRepository.DeleteAsync(id);
                return Ok("Contacto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el contacto: {ex.Message}");
            }
        }
    }
}
