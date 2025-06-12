using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un detalle específico dentro de una producción, asociado a una operación concreta.
    /// </summary>
    [Table("ProduccionDetalle")]
    public class ProduccionDetalle
    {
        /// <summary>
        /// Identificador único del detalle de producción.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Cantidad de unidades relacionadas con esta operación en la producción.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Identificador de la producción a la que pertenece este detalle.
        /// </summary>
        public int ProduccionId { get; set; }

        /// <summary>
        /// Identificador de la operación realizada en este detalle.
        /// </summary>
        public int OperacionId { get; set; }

        /// <summary>
        /// Valor total calculado para este detalle (Cantidad * ValorUnitario de la operación).
        /// </summary>
        public decimal? ValorTotal { get; set; }

        /// <summary>
        /// Producción asociada a este detalle.
        /// </summary>
        [JsonIgnore]
        public Produccion Produccion { get; set; }

        /// <summary>
        /// Operación asociada a este detalle de producción.
        /// </summary>
        public Operacion Operacion { get; set; }


    }
}
