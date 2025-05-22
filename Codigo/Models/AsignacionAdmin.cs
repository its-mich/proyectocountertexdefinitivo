using System.ComponentModel.DataAnnotations;

namespace CounterTexAPI.Models
{
    public class Asignacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TipoPrenda { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public string Prioridad { get; set; }

        [Required]
        public string AsignadoA { get; set; }

        public string Comentarios { get; set; }
    }
}
