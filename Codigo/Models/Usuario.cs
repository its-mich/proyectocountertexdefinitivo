using proyectocountertexdefinitivo.Models;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nombres { get; set; }

        [Required, MaxLength(100)]
        public string Apellidos { get; set; }

        [Required, MaxLength(20)]
        public string Documento { get; set; }

        [Required, MaxLength(100)]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        [EnumDataType(typeof(RolUsuario))]
        public RolUsuario Rol { get; set; }

        public int Edad { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        public ICollection<Produccion> Producciones { get; set; }
        public ICollection<Horario> Horarios { get; set; }
        public ICollection<Meta> Metas { get; set; }
        public ICollection<MensajeChat> MensajesEnviados { get; set; }
        public ICollection<MensajeChat> MensajesRecibidos { get; set; }
    }

    public enum RolUsuario
    {
        Administrador,
        Empleado
    }

}
