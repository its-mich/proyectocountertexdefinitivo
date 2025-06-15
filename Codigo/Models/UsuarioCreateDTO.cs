using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Data Transfer Object para la creación de un nuevo usuario.
    /// Contiene la información necesaria para registrar un usuario en el sistema.
    /// </summary>
    public class UsuarioCreateDTO
    {
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }

    }
}
