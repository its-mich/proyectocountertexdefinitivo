

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectocountertexdefinitivo.Models
{
    public class PerfilAdministrador
    {
        [Key]
        public int IdAdministrador { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        public int ProduccionDiaria { get; set; }
        public int ProduccionMensual { get; set; }
        public int ControlPrendas { get; set; }

        [MaxLength(100)]
        public string Registro { get; set; }

        public decimal Ganancias { get; set; }
        public decimal Pagos { get; set; }
        public decimal Gastos { get; set; }
        public decimal MetaPorCorte { get; set; }

        public DateTime ControlHorarios { get; set; }

        [MaxLength(200)]
        public string ChatInterno { get; set; }

        [MaxLength(100)]
        public string Proveedor { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

}
