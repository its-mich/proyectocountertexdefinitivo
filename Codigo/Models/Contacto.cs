namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un contacto con información básica de contacto y observaciones.
    /// </summary>
    public class Contacto
    {
        /// <summary>
        /// Identificador único del contacto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre completo de la persona de contacto.
        /// </summary>
        public string NombreCompleto { get; set; }

        /// <summary>
        /// Número telefónico del contacto.
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Correo electrónico del contacto.
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Observaciones adicionales sobre el contacto.
        /// </summary>
        public string Observacion { get; set; }
    }
}
