using proyectocountertexdefinitivo.Models;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un usuario del sistema, con información personal, rol y relaciones a operaciones y producciones.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Documento de identidad del usuario (requerido).
        /// </summary>
        [Required]
        public string? Documento { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Contraseña del usuario para autenticación.
        /// </summary>
        public string Contraseña { get; set; }

        /// <summary>
        /// Rol o perfil del usuario en el sistema.
        /// </summary>
        public string Rol { get; set; }

        /// <summary>
        /// Identificador de la operación asociada al usuario (opcional).
        /// </summary>
        public int? OperacionId { get; set; }

        /// <summary>
        /// Edad del usuario (opcional).
        /// </summary>
        public int? Edad { get; set; }

        /// <summary>
        /// Teléfono de contacto del usuario (opcional).
        /// </summary>
        public string? Telefono { get; set; }

        /// <summary>
        /// Operación asociada al usuario.
        /// </summary>
        public Operacion? Operacion { get; set; }

        /// <summary>
        /// Colección de producciones relacionadas con el usuario.
        /// </summary>
        public ICollection<Produccion> Producciones { get; set; }

        /// <summary>
        /// Colección de horarios asociados al usuario.
        /// </summary>
        public ICollection<Horario> Horarios { get; set; }

        /// <summary>
        /// Colección de metas asignadas al usuario.
        /// </summary>
        public ICollection<Meta> Metas { get; set; }

        /// <summary>
        /// Colección de mensajes enviados por el usuario.
        /// </summary>
        public ICollection<MensajeChat> MensajesEnviados { get; set; }

        /// <summary>
        /// Colección de mensajes recibidos por el usuario.
        /// </summary>
        public ICollection<MensajeChat> MensajesRecibidos { get; set; }
    }
}
