using Microsoft.EntityFrameworkCore.Metadata.Internal;
using proyectocountertexdefinitivo.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectocountertexdefinitivo.Models
{
    public class Token
    {
        [Key] // Esto marca a TokenValue como la clave primaria
        public string TokenValue { get; set; }

        public string Rol { get; set; } // <-- asegúrate que esto venga desde la API
    }
}
