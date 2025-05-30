using proyectocountertexdefinitivo.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        [Required]
        public string? Documento { get; set; }

        public string Correo { get; set; }

        /// <summary>
        /// Contraseña del usuario para autenticación.
        /// </summary>
        [JsonIgnore]
        public string Contraseña { get; set; }

        public string Rol { get; set; }

        /// <summary>
        /// Identificador de la operación asociada al usuario (opcional).
        /// </summary>
        [JsonIgnore]
        public int? OperacionId { get; set; }

        /// <summary>
        /// Edad del usuario (opcional).
        /// </summary>
        [JsonIgnore]
        public int? Edad { get; set; }

        /// <summary>
        /// Teléfono de contacto del usuario (opcional).
        /// </summary>
        [JsonIgnore]
        public string? Telefono { get; set; }

        /// <summary>
        /// Operación asociada al usuario.
        /// </summary>

        [JsonIgnore]
        public Operacion? Operacion { get; set; }

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
