namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// DTO para la creación de una nueva operación.
    /// </summary>
    public class OperacionCreateDTO
    {
        /// <summary>
        /// Nombre descriptivo de la operación.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Valor unitario asignado a la operación.
        /// </summary>
        public decimal ValorUnitario { get; set; }
    }
}
