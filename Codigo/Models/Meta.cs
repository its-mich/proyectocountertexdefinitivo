using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectocountertexdefinitivo.Models
{
    public class Meta
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int MetaCorte { get; set; }

        public int ProduccionReal { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }  // Relación con Usuario (por ejemplo, quien realiza la meta)

        public int RemitenteId { get; set; }
        public Usuario Remitente { get; set; }  // Relación con Remitente (quien envió el mensaje)

        public int DestinatarioId { get; set; }
        public Usuario Destinatario { get; set; }  // Relación con Destinatario (quien recibe el mensaje)

        public DateTime FechaHora { get; set; }

        public string Mensaje { get; set; }
    }
}
