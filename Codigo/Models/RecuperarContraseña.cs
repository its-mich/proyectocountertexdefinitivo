using Newtonsoft.Json;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// DTO utilizado para restablecer la contraseña de un usuario mediante un código de recuperación.
    /// </summary>
    public class RecuperarContraseña
    {
        /// <summary>
        /// Código de verificación enviado al correo del usuario.
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Nueva contraseña que el usuario desea establecer.
        /// </summary>
        [JsonProperty("nuevaContraseña")]
        public string NuevaContraseña { get; set; } = string.Empty;
    }
}
