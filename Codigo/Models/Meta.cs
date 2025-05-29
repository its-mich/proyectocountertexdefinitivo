namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa una meta de producción asignada a un usuario, con detalles de fechas y mensajes.
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// Identificador único de la meta.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha a la que corresponde la meta.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Meta de corte establecida.
        /// </summary>
        public int MetaCorte { get; set; }

        /// <summary>
        /// Producción real lograda.
        /// </summary>
        public int ProduccionReal { get; set; }

        /// <summary>
        /// Identificador del usuario al que se le asigna la meta.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Usuario asociado a la meta.
        /// </summary>
        public Usuario Usuario { get; set; }

        /// <summary>
        /// Identificador del remitente del mensaje relacionado.
        /// </summary>
        public int RemitenteId { get; set; }

        /// <summary>
        /// Usuario remitente del mensaje.
        /// </summary>
        public Usuario Remitente { get; set; }

        /// <summary>
        /// Identificador del destinatario del mensaje relacionado.
        /// </summary>
        public int DestinatarioId { get; set; }

        /// <summary>
        /// Usuario destinatario del mensaje.
        /// </summary>
        public Usuario Destinatario { get; set; }

        /// <summary>
        /// Fecha y hora en que se registró el mensaje o meta.
        /// </summary>
        public DateTime FechaHora { get; set; }

        /// <summary>
        /// Mensaje o descripción de la meta.
        /// </summary>
        public string Mensaje { get; set; }
    }
}
