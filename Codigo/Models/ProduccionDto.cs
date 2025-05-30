namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// DTO para transferir información simplificada de una producción.
    /// </summary>
    public class ProduccionDTO
    {
        /// <summary>
        /// Identificador único de la producción.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha en la que se realizó la producción.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Nombre del usuario responsable de la producción.
        /// </summary>
        public string Usuario { get; set; }

        /// <summary>
        /// Nombre de la prenda relacionada con la producción.
        /// </summary>
        public string Prenda { get; set; }

        /// <summary>
        /// Total producido (cantidad total o valor total, según contexto).
        /// </summary>
        public int Total { get; set; }
    }
}
