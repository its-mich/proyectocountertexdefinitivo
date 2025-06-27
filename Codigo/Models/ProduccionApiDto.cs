namespace proyectocountertexdefinitivo.Models
{
    public class ProduccionApiDto
    {

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int PrendaId { get; set; }
        public string PrendaNombre { get; set; }
        public List<ProduccionDetalleDto> ProduccionDetalles { get; set; }
        public decimal? TotalValor { get; set; }

    }
}
