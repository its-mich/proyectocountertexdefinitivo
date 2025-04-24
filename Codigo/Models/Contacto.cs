using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Contacto
    {
       
            public int Id { get; set; }

            public string NombreCompleto { get; set; }

            public string Telefono { get; set; }

            public string Correo { get; set; }

            public string Observacion { get; set; }
        

    }
}
