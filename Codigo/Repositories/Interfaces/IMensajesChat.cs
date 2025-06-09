using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para el manejo de mensajes de chat, que incluye operaciones CRUD y consultas específicas.
    /// </summary>
    public interface IMensajesChat
    {
        /// <summary>
        /// Obtiene todos los mensajes de chat.
        /// </summary>
        /// <returns>Una colección enumerable de objetos MensajeChat.</returns>
        Task<IEnumerable<MensajeChat>> ObtenerTodosAsync();

        /// <summary>
        /// Obtiene un mensaje de chat por su identificador.
        /// </summary>
        /// <param name="id">Identificador del mensaje.</param>
        /// <returns>El objeto MensajeChat correspondiente al Id proporcionado.</returns>
        Task<MensajeChat> ObtenerPorIdAsync(int id);

        Task<IEnumerable<MensajeChat>> ObtenerConversacionAsync(int remitenteId, int destinatarioId);

        /// <summary>
        /// Obtiene todos los mensajes enviados por un usuario específico.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario remitente.</param>
        /// <returns>Una colección enumerable de mensajes enviados por el usuario.</returns>
        Task<IEnumerable<MensajeChat>> ObtenerPorUsuarioAsync(int usuarioId);

        /// <summary>
        /// Agrega un nuevo mensaje de chat.
        /// </summary>
        /// <param name="mensaje">Objeto MensajeChat a agregar.</param>
        Task AgregarAsync(MensajeChat mensaje);

        /// <summary>
        /// Actualiza un mensaje de chat existente.
        /// </summary>
        /// <param name="mensaje">Objeto MensajeChat con los datos actualizados.</param>
        Task ActualizarAsync(MensajeChat mensaje);

        /// <summary>
        /// Elimina un mensaje de chat por su identificador.
        /// </summary>
        /// <param name="id">Identificador del mensaje a eliminar.</param>
        Task EliminarAsync(int id);
    }
}
