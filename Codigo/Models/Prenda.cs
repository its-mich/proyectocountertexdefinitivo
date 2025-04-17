

using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Prenda
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public string Genero { get; set; } // Puede validarse con enum si se desea

        [MaxLength(50)]
        public string Color { get; set; }

        public ICollection<Produccion> Producciones { get; set; }
    }

}
