    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace proyectocountertexdefinitivo.Models
    {
        public class ProduccionDetalle
        {
            public int Id { get; set; }

            public int Cantidad { get; set; }

            public int ProduccionId { get; set; }

            public Produccion Produccion { get; set; }

            public int OperacionId { get; set; }

            public Operacion Operacion { get; set; }

            public decimal? ValorTotal { get; set; }

        }
    }
