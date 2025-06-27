using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// DTO utilizado para registrar un nuevo pago a proveedor.
    /// Contiene los campos necesarios para calcular el total pagado.
    /// </summary>
    public class PagoProveedorDTO
    {
        /// <summary>
        /// Identificador del proveedor al que se realiza el pago.
        /// </summary>
        [Required(ErrorMessage = "El ID del proveedor es obligatorio.")]
        public int ProveedorId { get; set; }

        /// <summary>
        /// Cantidad de prendas entregadas por el proveedor.
        /// </summary>
        [Required(ErrorMessage = "La cantidad de prendas es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int CantidadPrendas { get; set; }

        /// <summary>
        /// Precio pagado por cada prenda.
        /// </summary>
        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal PrecioUnitario { get; set; }

        /// <summary>
        /// Observaciones o notas adicionales sobre el pago.
        /// </summary>
        [MaxLength(500)]
        public string Observaciones { get; set; }
    }
}
