namespace proyectocountertexdefinitivo.Models
{
    public class UsuarioCreateDTO
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Documento { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; }
        public int? OperacionId { get; set; }
        public int? Edad { get; set; }
        public string Telefono { get; set; }
    }
}
