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

        public int Cantidad { get; set; }

        public int ProduccionId { get; set; }

        /// <summary>
        /// Producción asociada a este detalle.
        /// </summary>
        [JsonIgnore]
        public Produccion Produccion { get; set; }

            public int OperacionId { get; set; }

            public Operacion Operacion { get; set; }

            public decimal? ValorTotal { get; set; }

        }
    }
