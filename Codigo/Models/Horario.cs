namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un horario registrado para un empleado, indicando tipo, hora y fecha.
    /// </summary>
    public class Horario
    {
        /// <summary>
        /// Identificador único del horario.
        /// </summary>
        public int HorarioId { get; set; }

        /// <summary>
        /// Identificador del empleado asociado al horario.
        /// </summary>
        public int EmpleadoId { get; set; }

        /// <summary>
        /// Tipo de horario (por ejemplo: entrada, salida, descanso).
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Hora del registro del horario.
        /// </summary>
        public TimeSpan Hora { get; set; }

        /// <summary>
        /// Fecha del registro del horario.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Observaciones adicionales sobre el horario.
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Referencia al usuario (empleado) relacionado con este horario.
        /// </summary>
        public Usuario Usuario { get; set; }
    }
}
