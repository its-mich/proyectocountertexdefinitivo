using System.ComponentModel.DataAnnotations;

namespace CounterTexAPI.Models
{
    public class AsignacionEmpleado
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        public string Prioridad { get; set; }

        [Required]
        public string Estado { get; set; }

        public int? OperadorId { get; set; }

        [StringLength(500)]
        public string Comentarios { get; set; }
    }
}
