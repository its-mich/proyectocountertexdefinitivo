using System.Security.Claims;

namespace proyectocountertexdefinitivo.Models
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }

        // Relaciones
        public ICollection<Satelite> Satelites { get; set; }
        public string ConfirmarClave { get;  set; }
        public string Contraseña { get; set; }
        public string TipoUsuario { get; set; }
    }
}
