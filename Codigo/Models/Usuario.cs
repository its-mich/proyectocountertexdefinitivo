using proyectocountertexdefinitivo.Models;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        [Required]
        public string? Documento { get; set; }

        public string Correo { get; set; }

        public string Contraseña { get; set; }

        public string Rol { get; set; }

        public int? OperacionId { get; set; }

        public int? Edad { get; set; }

        public string? Telefono { get; set; }

        public Operacion? Operacion { get; set; }

        public ICollection<Produccion> Producciones { get; set; }
        public ICollection<Horario> Horarios { get; set; }
        public ICollection<Meta> Metas { get; set; }
        public ICollection<MensajeChat> MensajesEnviados { get; set; }
        public ICollection<MensajeChat> MensajesRecibidos { get; set; }
    }

}
