using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class OperacionEmpleado
    {
        [Key]
        public int IdOperacionEmpleado { get; set; }

        [ForeignKey("PerfilEmpleado")]
        public int IdEmpleado { get; set; }

        [ForeignKey("Operacion")]
        public int IdOperacion { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public virtual PerfilEmpleado Empleado { get; set; }
        public virtual Operacion Operacion { get; set; }
    }
}
