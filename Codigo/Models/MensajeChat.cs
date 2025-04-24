using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class MensajeChat
    {
       
            [Key]
            public int Id { get; set; }

            public int RemitenteId { get; set; }

            [ForeignKey("RemitenteId")]
            public Usuario Remitente { get; set; }

            public int DestinatarioId { get; set; }

            [ForeignKey("DestinatarioId")]
            public Usuario Destinatario { get; set; }

            public DateTime FechaHora { get; set; }

            [Required]
            public string Mensaje { get; set; }
        

    }
}
