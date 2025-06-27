using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un usuario del sistema, ya sea administrador, empleado o proveedor.
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
        /// Documento de identidad del usuario.
        /// </summary>
        public string? Documento { get; set; }

        /// <summary>
        /// Dirección de correo electrónico del usuario.
        /// </summary>
        [Required]
        public string Correo { get; set; }

        /// <summary>
        /// Contraseña cifrada del usuario.
        /// </summary>
        public string? Contraseña { get; set; }

        /// <summary>
        /// Identificador del rol asignado al usuario.
        /// </summary>
        public int RolId { get; set; }

        /// <summary>
        /// Edad del usuario.
        /// </summary>
        public int? Edad { get; set; }

        /// <summary>
        /// Número de teléfono del usuario.
        /// </summary>
        public string? Telefono { get; set; }

        /// <summary>
        /// Rol asociado al usuario.
        /// </summary>
        [JsonIgnore]
        public Rol? Rol { get; set; }

        /// <summary>
        /// Nombre del rol (no mapeado en la base de datos).
        /// </summary>
        [NotMapped]
        public string RolNombre { get; set; }

        /// <summary>
        /// Token temporal para recuperación de contraseña.
        /// </summary>
        [JsonIgnore]
        public string? TokenRecuperacion { get; set; }

        /// <summary>
        /// Fecha de expiración del token de recuperación.
        /// </summary>
        [JsonIgnore]
        public DateTime? TokenExpiracion { get; set; }

        /// <summary>
        /// Producciones realizadas por el usuario.
        /// </summary>
        [JsonIgnore]
        public ICollection<Produccion> Producciones { get; set; }

        /// <summary>
        /// Horarios laborales del usuario.
        /// </summary>
        [JsonIgnore]
        public ICollection<Horario> Horarios { get; set; }

        /// <summary>
        /// Metas asignadas al usuario.
        /// </summary>
        [JsonIgnore]
        public ICollection<Meta> MetasAsignadas { get; set; }

        /// <summary>
        /// Mensajes enviados por el usuario en el chat.
        /// </summary>
        [JsonIgnore]
        public ICollection<MensajeChat> MensajesEnviados { get; set; }

        /// <summary>
        /// Mensajes recibidos por el usuario en el chat.
        /// </summary>
        [JsonIgnore]
        public ICollection<MensajeChat> MensajesRecibidos { get; set; }

        /// <summary>
        /// Pagos recibidos por el usuario.
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Pago> Pagos { get; set; }

        /// <summary>
        /// Pagos realizados a proveedores (cuando el usuario actúa como proveedor).
        /// </summary>
        [JsonIgnore]
        public ICollection<PagoProveedor>? PagosProveedor { get; set; }

        /// <summary>
        /// Constructor que inicializa las colecciones de navegación.
        /// </summary>
        public Usuario()
        {
            Producciones = new List<Produccion>();
            Horarios = new List<Horario>();
            MetasAsignadas = new List<Meta>();
            MensajesEnviados = new List<MensajeChat>();
            MensajesRecibidos = new List<MensajeChat>();
            Pagos = new List<Pago>();
        }
    }
}
