using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class PerfilEmpleado
    {
        [Key]
        public int IdEmpleado { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<OperacionEmpleado> OperacionesRealizadas { get; set; }
    }
}
