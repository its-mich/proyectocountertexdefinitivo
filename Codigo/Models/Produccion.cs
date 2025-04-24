using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectocountertexdefinitivo.Models
{

    [Table("Produccion")]  // Especifica explícitamente el nombre de la tabla
    public class Produccion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public decimal TotalValor { get; set; }

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public int PrendaId { get; set; }

        public Prenda Prenda { get; set; }

        public ICollection<ProduccionDetalle> ProduccionDetalles { get; set; }
    }
}
