namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un mensaje en el chat entre usuarios.
    /// </summary>
    public class MensajeChat
    {
        /// <summary>
        /// Identificador único del mensaje.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador del usuario remitente.
        /// </summary>
        public int RemitenteId { get; set; }

        /// <summary>
        /// Usuario remitente del mensaje.
        /// </summary>
        public Usuario Remitente { get; set; }

        /// <summary>
        /// Identificador del usuario destinatario.
        /// </summary>
        public int DestinatarioId { get; set; }

        /// <summary>
        /// Usuario destinatario del mensaje.
        /// </summary>
        public Usuario Destinatario { get; set; }

        /// <summary>
        /// Fecha y hora en que se envió el mensaje.
        /// </summary>
        public DateTime FechaHora { get; set; }

        /// <summary>
        /// Contenido del mensaje enviado.
        /// </summary>
        public string Mensaje { get; set; }
    }
}
