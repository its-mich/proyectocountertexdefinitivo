

using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Prenda
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Genero { get; set; } // Puede validarse con enum si se desea

        public string Color { get; set; }

        public ICollection<Produccion> Producciones { get; set; }
    }

}
