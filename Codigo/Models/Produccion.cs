using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{

    [Table("Produccion")]  // Especifica explícitamente el nombre de la tabla
    public class Produccion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public decimal TotalValor { get; set; }

        public int UsuarioId { get; set; }

        /// <summary>
        /// Usuario que realizó la producción.
        /// </summary>
        [JsonIgnore]
        public Usuario Usuario { get; set; }

        public int PrendaId { get; set; }

        public Prenda Prenda { get; set; }

        /// <summary>
        /// Detalles de la producción que describen las operaciones realizadas.
        /// </summary>
        public List<ProduccionDetalle> ProduccionDetalles { get; set; }
    }
}
