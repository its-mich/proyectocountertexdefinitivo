using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador que gestiona las operaciones CRUD sobre los contactos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Contexto de la base de datos de CounterTex.</param>
        public ContactoController(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de todos los contactos registrados.
        /// </summary>
        /// <returns>Lista de objetos <see cref="Contacto"/>.</returns>
        /// <response code="200">Lista de contactos obtenida correctamente.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetContactos()
        {
            return await _context.Contacto.ToListAsync();
        }

        /// <summary>
        /// Obtiene un contacto específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del contacto.</param>
        /// <returns>Un objeto <see cref="Contacto"/> si se encuentra; de lo contrario, <see cref="NotFound"/>.</returns>
        /// <response code="200">Contacto encontrado.</response>
        /// <response code="404">Contacto no encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Contacto>> GetContacto(int id)
        {
            var contacto = await _context.Contacto.FindAsync(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return contacto;
        }

        /// <summary>
        /// Crea un nuevo contacto en la base de datos.
        /// </summary>
        /// <param name="contacto">Objeto <see cref="Contacto"/> con los datos del nuevo contacto.</param>
        /// <returns>El contacto creado con una respuesta 201 y la ubicación del recurso.</returns>
        /// <response code="201">Contacto creado correctamente.</response>
        [HttpPost]
        public async Task<ActionResult<Contacto>> PostContacto(Contacto contacto)
        {
            _context.Contacto.Add(contacto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContacto", new { id = contacto.Id }, contacto);
        }

        /// <summary>
        /// Elimina un contacto existente por su identificador.
        /// </summary>
        /// <param name="id">Identificador del contacto a eliminar.</param>
        /// <returns><see cref="NoContent"/> si se eliminó correctamente; de lo contrario, <see cref="NotFound"/>.</returns>
        /// <response code="204">Contacto eliminado correctamente.</response>
        /// <response code="404">Contacto no encontrado.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            var contacto = await _context.Contacto.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }

            _context.Contacto.Remove(contacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
