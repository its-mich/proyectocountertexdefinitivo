using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class PagoProveedor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProveedorId { get; set; }

        [Required]
        public int CantidadPrendas { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal TotalPagado { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [MaxLength(500)]
        public string Observaciones { get; set; }

        // Relación con Usuario (Proveedor)
        [ForeignKey("ProveedorId")]
        public Usuario Proveedor { get; set; }
    }
}
