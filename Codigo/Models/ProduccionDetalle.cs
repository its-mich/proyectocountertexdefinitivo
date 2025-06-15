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
        public int Id { get; set; }
        public int Cantidad { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ProduccionId { get; set; }
        public int OperacionId { get; set; }

        // ValorTotal no se envía desde el cliente, lo puedes calcular en el backend
        [JsonIgnore]
        public decimal? ValorTotal { get; set; }

        [JsonIgnore]
        public Produccion? Produccion { get; set; }

        [JsonIgnore]
        public Operacion? Operacion { get; set; }
    }
}
