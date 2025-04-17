using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using proyectocountertexdefinitivo.Controllers;

namespace proyectocountertexdefinitivo.Models
{
    public class Produccion
    {
        //[Key]
        //public int Id { get; set; }

        //[Required]
        //public int ProduccionId { get; set; }

        //[ForeignKey("ProduccionId")]
        //public  Produccion Producciones { get; set; }

        //[Required]
        //public int OperacionId { get; set; }

        //[ForeignKey("OperacionId")]
        //public Operacion Operacion { get; set; }

        //public int Cantidad { get; set; }

        //[NotMapped]
        //public decimal ValorTotal => Cantidad * (Operacion?.ValorUnitario ?? 0);

        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalValor { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int PrendaId { get; set; }
        public Prenda Prenda { get; set; }
    }


}
