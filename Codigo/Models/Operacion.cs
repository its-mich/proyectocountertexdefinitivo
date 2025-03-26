using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Operacion
    {

        [Key]
        public int IdOperacion { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        public virtual ICollection<OperacionEmpleado> OperacionesEmpleados { get; set; }
    }
}
