using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa una producción realizada en el sistema.
    /// </summary>
    [Table("Produccion")]
    public class Produccion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalValor { get; set; }
        public int UsuarioId { get; set; }
        public int PrendaId { get; set; }
        
        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        [JsonIgnore]
        public Prenda? Prenda { get; set; }


        public ICollection<ProduccionDetalle> ProduccionDetalles { get; set; } = new List<ProduccionDetalle>();
    }
}
