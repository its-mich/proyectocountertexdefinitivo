using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa una prenda de vestir dentro del sistema.
    /// </summary>
    public class Prenda
    {
        /// <summary>
        /// Identificador único de la prenda.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre descriptivo de la prenda.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Género al que está dirigida la prenda (por ejemplo: masculino, femenino, unisex).
        /// </summary>
        public string Genero { get; set; } // Puede validarse con enum si se desea

        /// <summary>
        /// Color predominante de la prenda.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Colección de producciones asociadas a esta prenda.
        /// </summary>
        [JsonIgnore]
        public ICollection<Produccion> Producciones { get; set; }
    }
}
