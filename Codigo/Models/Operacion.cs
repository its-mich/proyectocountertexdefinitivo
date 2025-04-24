using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Operacion
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal? ValorUnitario { get; set; }

        public ICollection<ProduccionDetalle> ProduccionDetalles { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }

}
