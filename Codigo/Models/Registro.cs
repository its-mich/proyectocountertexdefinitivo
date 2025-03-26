using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{

    public class Registro
    {
        [Key]
        public int IdRegistro { get; set; }

        [Required, MaxLength(100)]
        public string Nombres { get; set; }

        [Required, MaxLength(100)]
        public string Apellidos { get; set; }

        [Required, MaxLength(50)]
        public string Documento { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Correo { get; set; }

        [Required, MaxLength(100)]
        public string Contraseña { get; set; }

        public DateTime FechaRegistro { get; set; }
    }

}
