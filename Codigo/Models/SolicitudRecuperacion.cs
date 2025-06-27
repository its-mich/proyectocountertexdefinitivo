namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Modelo para solicitar un código de recuperación de contraseña.
    /// </summary>
    public class SolicitudRecuperacion
    {
        /// <summary>
        /// Correo electrónico del usuario que solicita la recuperación.
        /// </summary>
        public string Correo { get; set; }
    }
}
