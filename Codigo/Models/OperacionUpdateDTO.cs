namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// DTO para actualizar una operación existente.
    /// </summary>
    public class OperacionUpdateDTO
    {
        /// <summary>
        /// Identificador único de la operación.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre descriptivo de la operación.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Valor unitario actualizado de la operación.
        /// </summary>
        public decimal ValorUnitario { get; set; }
    }
}
