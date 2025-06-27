using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un detalle específico dentro de una producción, asociado a una operación concreta.
    /// Contiene la cantidad producida y permite calcular el valor total.
    /// </summary>
    [Table("ProduccionDetalle")]
    public class ProduccionDetalle
    {
        /// <summary>
        /// Identificador único del detalle de producción.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Cantidad producida para la operación especificada.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Identificador de la producción a la que pertenece este detalle.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ProduccionId { get; set; }

        /// <summary>
        /// Identificador de la operación realizada en este detalle.
        /// </summary>
        public int OperacionId { get; set; }

        /// <summary>
        /// Valor total calculado del detalle (Cantidad x valor unitario de la operación).
        /// Este valor se calcula en el backend y no se espera que sea enviado por el cliente.
        /// </summary>
        [JsonIgnore]
        public decimal? ValorTotal { get; set; }

        /// <summary>
        /// Navegación a la entidad Producción.
        /// </summary>
        [JsonIgnore]
        public Produccion? Produccion { get; set; }

        /// <summary>
        /// Navegación a la entidad Operación.
        /// </summary>
        [JsonIgnore]
        public Operacion? Operacion { get; set; }
    }
}
