namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un rol dentro del sistema (por ejemplo: Administrador, Empleado).
    /// </summary>
    public class Rol
    {
        /// <summary>
        /// Identificador único del rol.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Colección de usuarios que tienen asignado este rol.
        /// </summary>
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
