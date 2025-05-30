namespace proyectocountertexdefinitivo.Models
{
    /// <summary>
    /// DTO para transferir información simplificada de una producción.
    /// </summary>
    public class ProduccionDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public string Prenda { get; set; }
        public int Total { get; set; }
    }
}
