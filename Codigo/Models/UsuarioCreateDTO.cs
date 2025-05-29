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
        /// Identificador del usuario (generalmente no se asigna en creación, pero está presente).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        [Required]
        public string Nombre { get; set; }

        /// <summary>
        /// Documento de identidad del usuario.
        /// </summary>
        [Required]
        public string? Documento { get; set; }

        /// <summary>
        /// Rol asignado al usuario (por ejemplo, administrador, empleado).
        /// </summary>
        [Required]
        public string Rol { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        /// <summary>
        /// Contraseña para la autenticación del usuario.
        /// </summary>
        [Required]
        public string Contraseña { get; set; }

        /// <summary>
        /// Edad del usuario (opcional).
        /// </summary>
        public int? Edad { get; set; }

        /// <summary>
        /// Teléfono de contacto del usuario (opcional).
        /// </summary>
        public int? Telefono { get; set; }

        /// <summary>
        /// Identificador de la operación asociada al usuario (opcional).
        /// </summary>
        public int? OperacionId { get; set; }
    }
}
