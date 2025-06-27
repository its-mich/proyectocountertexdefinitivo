using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class UsuarioUpdateDTO
    {
        public int Id { get; set; }

     
        public string Correo { get; set; }

      
        public string Telefono { get; set; }

    
        [Range(1, 120)]
        public int Edad { get; set; }
    }
}
