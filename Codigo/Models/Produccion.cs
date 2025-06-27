using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa una producción realizada en el sistema, que agrupa detalles de producción de prendas por parte de un usuario.
    /// </summary>
    [Table("Produccion")]
    public class Produccion
    {
        /// <summary>
        /// Identificador único de la producción.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha en que se registró la producción.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Valor total calculado de la producción.
        /// </summary>
        public decimal TotalValor { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó la producción.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Identificador de la prenda asociada a la producción.
        /// </summary>
        public int PrendaId { get; set; }

        /// <summary>
        /// Usuario relacionado con esta producción.
        /// </summary>
        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        /// <summary>
        /// Prenda asociada a la producción.
        /// </summary>
        [JsonIgnore]
        public Prenda? Prenda { get; set; }

        /// <summary>
        /// Lista de detalles que conforman esta producción.
        /// </summary>
        public ICollection<ProduccionDetalle> ProduccionDetalles { get; set; } = new List<ProduccionDetalle>();
    }
}
