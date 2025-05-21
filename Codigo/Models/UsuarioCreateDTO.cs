using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class UsuarioCreateDTO
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string? Documento { get; set; }

        [Required]
        public string Rol { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Contraseña { get; set; }

        public int? Edad { get; set; }         
        public int? Telefono { get; set; }   
        public int? OperacionId { get; set; }
    }
}
