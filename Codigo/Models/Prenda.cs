using System.Text.Json.Serialization;



using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Prenda
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Genero { get; set; } // Puede validarse con enum si se desea

        public string Color { get; set; }

        /// <summary>
        /// Colección de producciones asociadas a esta prenda.
        /// </summary>
        [JsonIgnore]
        public ICollection<Produccion> Producciones { get; set; }
    }

}
