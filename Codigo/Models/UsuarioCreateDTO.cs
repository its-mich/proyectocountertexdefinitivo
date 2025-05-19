namespace proyectocountertexdefinitivo.Models
{
    public class UsuarioCreateDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Documento { get; set; }
        public string Rol { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}
