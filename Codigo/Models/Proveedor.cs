using System.ComponentModel.DataAnnotations;

namespace proyectocountertexdefinitivo.Models
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [Required, MaxLength(150)]
        public string NombreProveedor { get; set; }

        public decimal PrecioPrenda { get; set; }

        [MaxLength(100)]
        public string TipoPrenda { get; set; }

        [MaxLength(50)]
        public string Telefono { get; set; }

        [MaxLength(200)]
        public string Direccion { get; set; }

        [MaxLength(100)]
        public string Ciudad { get; set; }

        [MaxLength(100)]
        public string Localidad { get; set; }

        [MaxLength(100)]
        public string Barrio { get; set; }

        public int CantidadPrendas { get; set; }
    }


}
