

using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class PerfilAdministrador
    {
        public int IdAdministrador { get; set; }
        public string NombreAdministrador { get; set; }


      
        public int ProduccionDiaria { get; set; }
        public int ProduccionMensual { get; set; }
        public int ControlPrendas { get; set; }
        public string Registro { get; set; }
        public decimal Ganancias { get; set; }
        public decimal Pagos { get; set; }
        public decimal Gastos { get; set; }
        public decimal MetaPorCorte { get; set; }
        public string ConsultarInformacion { get; set; }
        public string ControlHorarios { get; set; }
        public string ChatInterno { get; set; }
        public string Proveedor { get; set; }
        public string BotonAyuda { get; set; }

        ////Relaciones//
        public int IdUsuario { get; set; }
        //public Usuarios Usuario { get; set; }
    }
}
