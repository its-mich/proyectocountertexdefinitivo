using proyectocountertexdefinitivo.Models;
using System.Text.Json.Serialization;

public class Meta
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public int MetaCorte { get; set; }

    public int ProduccionReal { get; set; }

    public DateTime FechaHora { get; set; }

    public string Mensaje { get; set; }

    // Relación con Usuario (usuario asignado)
    public int UsuarioId { get; set; }
    
    [JsonIgnore]
    public Usuario? Usuario { get; set; }

    // Relación con Remitente
    public int RemitenteId { get; set; }

    [JsonIgnore]
    public Usuario? Remitente { get; set; }

    // Relación con Destinatario
    public int DestinatarioId { get; set; }

    [JsonIgnore]
    public Usuario? Destinatario { get; set; }
}
