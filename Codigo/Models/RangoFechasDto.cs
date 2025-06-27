namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un rango de fechas que se puede usar para filtrar datos por un periodo específico.
    /// </summary>
    public class RangoFechasDto
    {
        /// <summary>
        /// Fecha de inicio del rango.
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha de fin del rango.
        /// </summary>
        public DateTime FechaFin { get; set; }
    }
}
