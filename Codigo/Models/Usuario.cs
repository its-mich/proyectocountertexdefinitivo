using proyectocountertexdefinitivo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [Required]
        public string Nombre { get; set; }

        /// <summary>
        /// Documento de identidad del usuario (requerido).
        /// </summary>
        public string? Documento { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        [Required]
        public string Correo { get; set; }

        /// <summary>
        /// Contraseña del usuario para autenticación.
        /// </summary>

        [JsonIgnore]
        public string Contraseña { get; set; }

        /// <summary>
        /// Rol o perfil del usuario en el sistema.
        /// </summary>
        public int RolId { get; set; }

        /// <summary>
        /// Edad del usuario (opcional).
        /// </summary>
        public int? Edad { get; set; }

        /// <summary>
        /// Teléfono de contacto del usuario (opcional).
        /// </summary>
        public string? Telefono { get; set; }

        /// <summary>
        /// Rol asociado al usuario.
        /// </summary>
        [JsonIgnore]
        public Rol? Rol { get; set; }

        [NotMapped]
        public string RolNombre { get; set; }

        [JsonIgnore]
        public string? TokenRecuperacion { get; set; }

        [JsonIgnore]
        public DateTime? TokenExpiracion { get; set; }

        public Usuario()
        {
            Producciones = new List<Produccion>();
            Horarios = new List<Horario>();
            Metas = new List<Meta>();
            MensajesEnviados = new List<MensajeChat>();
            MensajesRecibidos = new List<MensajeChat>();
        }

        /// <summary>
        /// Colección de producciones relacionadas con el usuario.
        /// </summary>
        [JsonIgnore]
        public ICollection<Produccion> Producciones { get; set; }

        /// <summary>
        /// Colección de horarios asociados al usuario.
        /// </summary>
        [JsonIgnore]
        public ICollection<Horario> Horarios { get; set; }

        /// <summary>
        /// Colección de metas asignadas al usuario.
        /// </summary>
        [JsonIgnore]
        public ICollection<Meta> Metas { get; set; }

        /// <summary>
        /// Colección de mensajes enviados por el usuario.
        /// </summary>
        [JsonIgnore]
        public ICollection<MensajeChat> MensajesEnviados { get; set; }

        /// <summary>
        /// Colección de mensajes recibidos por el usuario.
        /// </summary>
        [JsonIgnore]
        public ICollection<MensajeChat> MensajesRecibidos { get; set; }
    }
}
