using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa una operación que puede estar relacionada con producciones y usuarios.
    /// </summary>
    public class Operacion
    {
        /// <summary>
        /// Identificador único de la operación.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre descriptivo de la operación.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Valor unitario asignado a la operación.
        /// </summary>
        public decimal? ValorUnitario { get; set; }

        /// <summary>
        /// Detalles de producción relacionados con esta operación.
        /// </summary>
        [JsonIgnore]
        public ICollection<ProduccionDetalle> ProduccionDetalles { get; set; }

        /// <summary>
        /// Usuarios relacionados con esta operación.
        /// </summary>
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
