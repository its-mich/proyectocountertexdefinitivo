namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Data Transfer Object (DTO) que representa un detalle de producción.
    /// Utilizado para enviar y recibir información simplificada de un detalle.
    /// </summary>
    public class ProduccionDetalleDto
    {
        /// <summary>
        /// Identificador de la operación realizada.
        /// </summary>
        public int OperacionId { get; set; }

        /// <summary>
        /// Cantidad de elementos producidos en la operación.
        /// </summary>
        public int Cantidad { get; set; }
    }
}
