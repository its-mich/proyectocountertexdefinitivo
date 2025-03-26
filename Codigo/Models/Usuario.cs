using proyectocountertexdefinitivo.Models;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{

    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required, MaxLength(50)]
        public string NombreUsuario { get; set; }

        [Required, EmailAddress, MaxLength(50)]
        public string Correo { get; set; }

        [Required, MaxLength(50)]
        public string Clave { get; set; }

        public virtual ICollection<PerfilEmpleado> Empleados { get; set; }
        public virtual ICollection<PerfilAdministrador> Administradores { get; set; }
        public virtual ICollection<Satelite> Satelites { get; set; }
    }
}
