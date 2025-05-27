namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un token de autenticación junto con el rol asociado.
    /// Este modelo no se persiste en la base de datos.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Valor del token de autenticación.
        /// </summary>
        [Key]
        public string TokenValue { get; set; }

        /// <summary>
        /// Rol asignado al token (debe provenir de la API).
        /// </summary>
        public string Rol { get; set; }
    }
}
