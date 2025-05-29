using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar los mensajes de chat.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MensajesChatController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto de base de datos.</param>
        public MensajesChatController(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los mensajes del chat.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="MensajeChat"/>.</returns>
        /// <response code="200">Lista de mensajes obtenida correctamente.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MensajeChat>>> GetMensajesChat()
        {
            return await _context.MensajesChat.ToListAsync();
        }

        /// <summary>
        /// Obtiene un mensaje del chat por su ID.
        /// </summary>
        /// <param name="id">ID del mensaje a buscar.</param>
        /// <returns>El mensaje correspondiente o <see cref="NotFound"/> si no existe.</returns>
        /// <response code="200">Mensaje encontrado correctamente.</response>
        /// <response code="404">Mensaje no encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<MensajeChat>> GetMensajeChat(int id)
        {
            var mensajeChat = await _context.MensajesChat.FindAsync(id);

            if (mensajeChat == null)
            {
                return NotFound();
            }

            return mensajeChat;
        }

        /// <summary>
        /// Crea un nuevo mensaje en el chat.
        /// </summary>
        /// <param name="mensajeChat">Objeto <see cref="MensajeChat"/> con los datos del mensaje.</param>
        /// <returns>El mensaje creado junto con su URI de acceso.</returns>
        /// <response code="201">Mensaje creado correctamente.</response>
        [HttpPost]
        public async Task<ActionResult<MensajeChat>> PostMensajeChat(MensajeChat mensajeChat)
        {
            _context.MensajesChat.Add(mensajeChat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMensajeChat", new { id = mensajeChat.Id }, mensajeChat);
        }

        /// <summary>
        /// Elimina un mensaje del chat por su ID.
        /// </summary>
        /// <param name="id">ID del mensaje a eliminar.</param>
        /// <returns><see cref="NoContent"/> si se elimina correctamente o <see cref="NotFound"/> si no existe.</returns>
        /// <response code="204">Mensaje eliminado correctamente.</response>
        /// <response code="404">Mensaje no encontrado.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensajeChat(int id)
        {
            var mensajeChat = await _context.MensajesChat.FindAsync(id);
            if (mensajeChat == null)
            {
                return NotFound();
            }

            _context.MensajesChat.Remove(mensajeChat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
