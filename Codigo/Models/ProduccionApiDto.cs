namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// DTO utilizado para exponer los datos de una producción a través de la API.
    /// Incluye información básica de la producción y sus detalles.
    /// </summary>
    public class ProduccionApiDto
    {
        /// <summary>
        /// Identificador único de la producción.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha en que se realizó la producción.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Identificador de la prenda asociada a la producción.
        /// </summary>
        public int PrendaId { get; set; }

        /// <summary>
        /// Nombre de la prenda asociada.
        /// </summary>
        public string PrendaNombre { get; set; }

        /// <summary>
        /// Lista de detalles de producción relacionados con esta producción.
        /// </summary>
        public List<ProduccionDetalleDto> ProduccionDetalles { get; set; }

        /// <summary>
        /// Valor total de la producción, calculado a partir de los detalles.
        /// Puede ser nulo si no se ha calculado.
        /// </summary>
        public decimal? TotalValor { get; set; }
    }
}
