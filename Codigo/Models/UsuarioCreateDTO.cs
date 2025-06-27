using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Data Transfer Object para la creación de un nuevo usuario.
    /// Contiene la información necesaria para registrar un usuario en el sistema.
    /// </summary>
    public class UsuarioCreateDTO
    {
        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Número de documento de identidad del usuario.
        /// </summary>
        public string Documento { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Contraseña del usuario (sin encriptar, se recomienda cifrarla en backend).
        /// </summary>
        public string Contraseña { get; set; }

        /// <summary>
        /// Edad del usuario.
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Número de teléfono del usuario.
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Identificador del rol asignado al usuario. Si no se especifica, se asignará por defecto.
        /// </summary>
        public int RolId { get; set; }
    }
}
