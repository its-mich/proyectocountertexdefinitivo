using proyectocountertexdefinitivo.Models;
using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Satelite
    {
  
        public int SateliteId { get; set; }
        public string Fabricante { get; set; }
        public decimal PagoPrendas { get; set; } 
        public decimal Ganancias{ get; set; }
        public string Operacion { get; set; }
        public decimal PagoOperacion { get; set; }
        public int Inventariomaquinas { get; set; }

        public string TipoMaquina { get; set; }
       // Relaciones

       public int IdUsuario { get; set; }
        //public Usuarios Usuario { get; set; }

    }
}
