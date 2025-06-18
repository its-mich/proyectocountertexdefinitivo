using Newtonsoft.Json;

namespace proyectocountertexdefinitivo.Models
{
    public class RecuperarContraseña
    {
        public string Codigo { get; set; } = string.Empty;

        [JsonProperty("nuevaContraseña")]
        public string NuevaContraseña { get; set; } = string.Empty;
    }
}
