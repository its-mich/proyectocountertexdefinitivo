using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar los mensajes de chat.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MensajesChatController : ControllerBase
    {
        private readonly IMensajesChat _mensajesChat;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto de base de datos.</param>
        public MensajesChatController(IMensajesChat mensajesChat)
        {
            _mensajesChat = mensajesChat;
        }

        /// <summary>
        /// Obtiene todos los mensajes del chat.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="MensajeChat"/>.</returns>
        /// <response code="200">Lista de mensajes obtenida correctamente.</response>
        [HttpGet]
        public async Task<IActionResult> GetMensajesChat()
        {
            try
            {
                var response = await _mensajesChat.ObtenerTodosAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno al obtener mensajes del chat",
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        /// <summary>
        /// Obtiene un mensaje del chat por su ID.
        /// </summary>
        /// <param name="id">ID del mensaje a buscar.</param>
        /// <returns>El mensaje correspondiente o <see cref="NotFound"/> si no existe.</returns>
        /// <response code="200">Mensaje encontrado correctamente.</response>
        /// <response code="404">Mensaje no encontrado.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMensajesChatById(int id)
        {
            try
            {
                var usuario = await _mensajesChat.ObtenerPorIdAsync(id);
                if (usuario == null)
                    return NotFound("Mensaje no encontrado.");
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al buscar el mensaje", error = ex.Message });
            }
        }

        [HttpGet("Conversacion")]
        public async Task<IActionResult> ObtenerMensajes([FromQuery] int remitenteId, [FromQuery] int destinatarioId)
        {
            try
            {
                var mensajes = await _mensajesChat.ObtenerConversacionAsync(remitenteId, destinatarioId);
                return Ok(mensajes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los mensajes", error = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo mensaje en el chat.
        /// </summary>
        /// <param name="mensajeChat">Objeto <see cref="MensajeChat"/> con los datos del mensaje.</param>
        /// <returns>El mensaje creado junto con su URI de acceso.</returns>
        /// <response code="201">Mensaje creado correctamente.</response>
        [HttpPost("EnviarMensaje")]
        public async Task<IActionResult> EnviarMensaje([FromBody] MensajeChat mensajeChat)
        {
            try
            {
                if (mensajeChat == null || string.IsNullOrEmpty(mensajeChat.Mensaje)
        || mensajeChat.RemitenteId == 0 || mensajeChat.DestinatarioId == 0)
                {
                    return BadRequest("Datos incompletos.");
                }

                mensajeChat.FechaHora = DateTime.Now;
                await _mensajesChat.AgregarAsync(mensajeChat);
                return Ok(new { message = "Mensaje enviado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al registrar el mensaje", error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un mensaje del chat por su ID.
        /// </summary>
        /// <param name="id">ID del mensaje a eliminar.</param>
        /// <returns><see cref="NoContent"/> si se elimina correctamente o <see cref="NotFound"/> si no existe.</returns>
        /// <response code="204">Mensaje eliminado correctamente.</response>
        /// <response code="404">Mensaje no encontrado.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensajesChat(int id)
        {
            try
            {
                var mensaje = await _mensajesChat.ObtenerPorIdAsync(id);
                if (mensaje == null)
                    return NotFound($"No se encontró ningún mensaje con el ID {id}. Verifique e intente de nuevo.");

                await _mensajesChat.EliminarAsync(id);
                return Ok($"Mensaje con ID {id} eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al eliminar el mensaje con ID {id}.", error = ex.Message });
            }
        }
    }
}
