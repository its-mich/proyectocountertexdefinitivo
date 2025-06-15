using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para las operaciones CRUD sobre la entidad Produccion.
    /// </summary>
    public interface IProduccion
    {
        /// <summary>
        /// Obtiene todas las producciones.
        /// </summary>
        /// <returns>Una colección enumerable de objetos Produccion.</returns>
        Task<IEnumerable<object>> GetAllAsync();

        /// <summary>
        /// Obtiene una producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la producción.</param>
        /// <returns>El objeto Produccion correspondiente al Id proporcionado.</returns>
        Task<Produccion> GetByIdAsync(int id);

        Task<Operacion?> ObtenerOperacionPorId(int operacionId);


        /// <summary>
        /// Crea una nueva producción.
        /// </summary>
        /// <param name="produccion">Objeto Produccion a crear.</param>
        /// <returns>La producción creada.</returns>
        Task<Produccion> CreateAsync(Produccion produccion);


        /// <summary>
        /// Crea una nueva producción con sus detalles calculando el valor total.
        /// </summary>
        /// <param name="produccion">Objeto Produccion con detalles incluidos.</param>
        /// <returns>La producción creada con los valores totales calculados, o null si falla.</returns>
        Task<Produccion?> CrearProduccionConDetallesAsync(Produccion produccion);


        /// <summary>
        /// Elimina una producción por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la producción a eliminar.</param>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Obtiene un resumen mensual de producción agrupado por prenda.
        /// </summary>
        /// <param name="anio">Año del resumen.</param>
        /// <param name="mes">Mes del resumen.</param>
        /// <returns>Un objeto con el resumen o null si no hay datos.</returns>
        Task<object> ObtenerResumenMensual(int anio, int mes);
    }
}
