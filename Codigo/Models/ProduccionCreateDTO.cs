namespace proyectocountertexdefinitivo.Models
{
    public class ProduccionCreateDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public int PrendaId { get; set; }
        public List<ProduccionDetalleDTO> ProduccionDetalles { get; set; }
    }

    public class ProduccionDetalleDTO
    {
        public int Id { get; set; }
        public int OperacionId { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorTotal { get; set; }
    }
}