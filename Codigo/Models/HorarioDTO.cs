namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// DTO para transferir información simplificada de horarios.
    /// </summary>
    public class HorarioDTO
    {
        /// <summary>
        /// Identificador del empleado.
        /// </summary>
        public int EmpleadoId { get; set; }

        /// <summary>
        /// Nombre completo del empleado.
        /// </summary>
        public string NombreEmpleado { get; set; }

        /// <summary>
        /// Fecha del horario.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Tipo de horario (entrada, salida, descanso).
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Hora del horario.
        /// </summary>
        public TimeSpan Hora { get; set; }

        /// <summary>
        /// Observaciones adicionales.
        /// </summary>
        public string Observaciones { get; set; }
    }
}
