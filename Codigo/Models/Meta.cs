using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Meta
    {

     
            //[Key]
            //public int Id { get; set; }

            //public int RemitenteId { get; set; }

            //[ForeignKey("RemitenteId")]
            //public Usuario Remitente { get; set; }

            //public int DestinatarioId { get; set; }

            //[ForeignKey("DestinatarioId")]
            //public Usuario Destinatario { get; set; }

            //public DateTime FechaHora { get; set; }

            //[Required]
            //public string Mensaje { get; set; }
            //public int MetaCorte { get; set; }
            //public int ProduccionReal { get; set; }

            //public int UsuarioId { get; set; }
            //public Usuario Usuario { get; set; }

            public int Id { get; set; }

            [Column(TypeName = "date")]
            public DateTime Fecha { get; set; }

            public int MetaCorte { get; set; }

            public int ProduccionReal { get; set; }

            public int UsuarioId { get; set; }
            public Usuario Usuario { get; set; }    

        




    }
}
