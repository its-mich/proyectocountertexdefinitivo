using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class PagoProveedorDTO
    {
        [Required(ErrorMessage = "El ID del proveedor es obligatorio.")]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "La cantidad de prendas es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int CantidadPrendas { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal PrecioUnitario { get; set; }

        [MaxLength(500)]
        public string Observaciones { get; set; }
    }
}
