namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa una producción realizada en el sistema.
    /// </summary>
    [Table("Produccion")]
    public class Produccion
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
        /// Valor total acumulado de la producción.
        /// </summary>
        public decimal TotalValor { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó la producción.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Usuario que realizó la producción.
        /// </summary>
        public Usuario Usuario { get; set; }

        /// <summary>
        /// Identificador de la prenda asociada a la producción.
        /// </summary>
        public int PrendaId { get; set; }

        /// <summary>
        /// Prenda asociada a la producción.
        /// </summary>
        public Prenda Prenda { get; set; }

        /// <summary>
        /// Detalles de la producción que describen las operaciones realizadas.
        /// </summary>
        public ICollection<ProduccionDetalle> ProduccionDetalles { get; set; }
    }
}
