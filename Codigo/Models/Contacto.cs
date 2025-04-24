using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Contacto
    {
       
            [Key]
            public int Id { get; set; }

            [Required]
            public string NombreCompleto { get; set; }

            [MaxLength(20)]
            public string Telefono { get; set; }

            [EmailAddress]
            public string Correo { get; set; }

            public string Observacion { get; set; }
        

    }
}
