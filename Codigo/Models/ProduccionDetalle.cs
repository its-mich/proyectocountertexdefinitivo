using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectocountertexdefinitivo.Models
{
    public class ProduccionDetalle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public int ProduccionId { get; set; }

        [ForeignKey("ProduccionId")]
        public Produccion Produccion { get; set; }

        [Required]
        public int OperacionId { get; set; }

        [ForeignKey("OperacionId")]
        public Operacion Operacion { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotal { get; set; }
    }
}
