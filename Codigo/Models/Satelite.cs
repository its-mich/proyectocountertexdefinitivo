using proyectocountertexdefinitivo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectocountertexdefinitivo.Models
{
    public class Satelite
    {
        [Key]
        public int IdSatelite { get; set; }

        [Required, MaxLength(100)]
        public string Fabricante { get; set; }

        public decimal PagoPrendas { get; set; }
        public decimal Ganancias { get; set; }
        public int InventarioMaquinas { get; set; }

        [MaxLength(50)]
        public string TipoMaquina { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

}
