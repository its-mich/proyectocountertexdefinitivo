namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IEmailService
    {
        Task EnviarCorreoAsync(string destinatario, string asunto, string contenido);
    }
}
