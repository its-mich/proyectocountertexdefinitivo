using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class ProduccionDetalle
    {
        public int Id { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public int ProduccionId { get; set; }
        public Produccion Produccion { get; set; }

        public int OperacionId { get; set; }
        public Operacion Operacion { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal ValorTotal { get; set; }
    }
}
