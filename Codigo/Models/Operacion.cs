using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Operacion
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorUnitario { get; set; }

        public ICollection<Registros> ProduccionDetalles { get; set; }
    }

}
