using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un registro de pago realizado a un usuario dentro del sistema.
    /// </summary>
    public class Pago
    {
        /// <summary>
        /// Identificador único del pago.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Identificador del usuario al que se le realizó el pago.
        /// </summary>
        [Required]
        public int UsuarioId { get; set; }

        /// <summary>
        /// Fecha de inicio del periodo trabajado.
        /// </summary>
        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha de fin del periodo trabajado.
        /// </summary>
        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Monto total pagado al usuario en el periodo.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPagado { get; set; }

        /// <summary>
        /// Fecha en la que se realizó el pago.
        /// </summary>
        public DateTime FechaPago { get; set; } = DateTime.Now;

        /// <summary>
        /// Observaciones adicionales relacionadas con el pago.
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Navegación hacia el usuario al que pertenece el pago.
        /// </summary>
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }
}
