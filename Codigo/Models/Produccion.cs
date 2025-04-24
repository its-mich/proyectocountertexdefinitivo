using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectocountertexdefinitivo.Models
{

    [Table("Produccion")]  // Especifica explícitamente el nombre de la tabla
    public class Produccion
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalValor { get; set; }

        // Clave foránea para Usuario
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]   
        public Usuario Usuario { get; set; }

        // Clave foránea para Prenda
        public int PrendaId { get; set; }

        [ForeignKey("PrendaId")]
        public Prenda Prenda { get; set; }

        // Relación: Una producción tiene muchos detalles
        public ICollection<ProduccionDetalle> ProduccionDetalles { get; set; }
    }
}
