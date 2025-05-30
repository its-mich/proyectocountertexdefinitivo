using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    public class Operacion
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal? ValorUnitario { get; set; }

        /// <summary>
        /// Detalles de producción relacionados con esta operación.
        /// </summary>
        [JsonIgnore]
        public ICollection<ProduccionDetalle> ProduccionDetalles { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }

}
