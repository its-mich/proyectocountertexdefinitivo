namespace proyectocountertexdefinitivo.Models
{
    public class RecuperarContraseña
    {
        public string Correo { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string NuevaContraseña { get; set; } = string.Empty;
    }
}
