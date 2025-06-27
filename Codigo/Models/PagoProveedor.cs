using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un pago realizado a un proveedor por la entrega de prendas.
    /// </summary>
    public class PagoProveedor
    {
        /// <summary>
        /// Identificador único del pago al proveedor.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Identificador del proveedor (usuario) al que se le realizó el pago.
        /// </summary>
        [Required]
        public int ProveedorId { get; set; }

        /// <summary>
        /// Cantidad de prendas entregadas por el proveedor.
        /// </summary>
        [Required]
        public int CantidadPrendas { get; set; }

        /// <summary>
        /// Precio unitario pagado por cada prenda.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecioUnitario { get; set; }

        /// <summary>
        /// Total pagado al proveedor (CantidadPrendas * PrecioUnitario).
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal TotalPagado { get; set; }

        /// <summary>
        /// Fecha en que se registró el pago.
        /// </summary>
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        /// <summary>
        /// Observaciones o notas adicionales sobre el pago.
        /// </summary>
        [MaxLength(500)]
        public string Observaciones { get; set; }

        /// <summary>
        /// Navegación al proveedor relacionado con el pago.
        /// </summary>
        [ForeignKey("ProveedorId")]
        public Usuario Proveedor { get; set; }
    }
}
