namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// Representa un resumen mensual de producciones realizadas,
    /// incluyendo la cantidad total y el valor total del mes especificado.
    /// </summary>
    public class ProduccionMensualResumenDTO
    {
        /// <summary>
        /// Cantidad total de producciones realizadas en el mes.
        /// </summary>
        public int TotalProducciones { get; set; }

        /// <summary>
        /// Suma total del valor de todas las producciones del mes.
        /// </summary>
        public decimal TotalValor { get; set; }

        /// <summary>
        /// Año correspondiente al resumen.
        /// </summary>
        public int Año { get; set; }

        /// <summary>
        /// Mes correspondiente al resumen (1-12).
        /// </summary>
        public int Mes { get; set; }
    }
}
