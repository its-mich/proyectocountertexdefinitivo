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
        [Key]
        public int IdToken { get; set; }

        [Required]
        public string TokenValue { get; set; }

        public DateTime FechaCreacion { get; set; }
    }

}
