using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class HorarioDTO
    {
        public int EmpleadoId { get; set; }

        public string NombreEmpleado { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public TimeSpan Hora { get; set; }

        public string Observaciones { get; set; }
    }
}
