using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class MensajeChat
    {
       
            public int Id { get; set; }

            public int RemitenteId { get; set; }

            public Usuario Remitente { get; set; }

            public int DestinatarioId { get; set; }

            public Usuario Destinatario { get; set; }

            public DateTime FechaHora { get; set; }

            public string Mensaje { get; set; }
        

    }
}
