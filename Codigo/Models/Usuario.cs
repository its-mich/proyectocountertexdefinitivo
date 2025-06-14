using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectocountertexdefinitivo.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string? Documento { get; set; }

        [Required]
        public string Correo { get; set; }

        public string Contraseña { get; set; }

        public int RolId { get; set; }

        public int? Edad { get; set; }

        public string? Telefono { get; set; }

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
            MetasAsignadas = new List<Meta>();
            MetasEnviadas = new List<Meta>();
            MetasRecibidas = new List<Meta>();
            MensajesEnviados = new List<MensajeChat>();
            MensajesRecibidos = new List<MensajeChat>();
        }

        [JsonIgnore]
        public ICollection<Produccion> Producciones { get; set; }

        [JsonIgnore]
        public ICollection<Horario> Horarios { get; set; }

        /// <summary>
        /// Metas asignadas al usuario (Meta.UsuarioId)
        /// </summary>
        [JsonIgnore]
        public ICollection<Meta> MetasAsignadas { get; set; }

        /// <summary>
        /// Metas enviadas por el usuario (Meta.RemitenteId)
        /// </summary>
        [JsonIgnore]
        public ICollection<Meta> MetasEnviadas { get; set; }

        /// <summary>
        /// Metas recibidas por el usuario (Meta.DestinatarioId)
        /// </summary>
        [JsonIgnore]
        public ICollection<Meta> MetasRecibidas { get; set; }

        [JsonIgnore]
        public ICollection<MensajeChat> MensajesEnviados { get; set; }

        [JsonIgnore]
        public ICollection<MensajeChat> MensajesRecibidos { get; set; }
    }
}
