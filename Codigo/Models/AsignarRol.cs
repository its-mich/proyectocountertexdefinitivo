namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Petición para asignar un nuevo rol a un usuario.
    /// 
    /// Roles disponibles:
    /// 1 = Administrador  
    /// 2 = Empleado  
    /// 3 = Proveedor  
    /// </summary>
    public class AsignarRol
    {
        /// <summary>
        /// Id del nuevo rol a asignar.
        /// 1 = Administrador, 2 = Empleado, 3 = Proveedor
        /// </summary>
        public int IdNuevoRol { get; set; }
    }
}