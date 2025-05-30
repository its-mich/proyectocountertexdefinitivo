using proyectocountertexdefinitivo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    public class Horario
    {
        public int HorarioId { get; set; }
        public int EmpleadoId { get; set; }
        public string Tipo { get; set; } // entrada, salida, descanso
        public TimeSpan Hora { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }

        [JsonIgnore]
        public Usuario Usuario { get; set; }
    }

}
