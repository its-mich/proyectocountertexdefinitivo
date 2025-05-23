using proyectocountertexdefinitivo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    public class Horario
    {
        [Key]
        public int HorarioId { get; set; }  // nuevo campo ID único

        public int EmpleadoId { get; set; }
        public string Tipo { get; set; } // entrada, salida, descanso
        public TimeSpan Hora { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }

        public Usuario Usuario { get; set; }
    }

}
