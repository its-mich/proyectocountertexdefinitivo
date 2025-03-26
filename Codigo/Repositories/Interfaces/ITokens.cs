using proyectocountertexdefinitivo.Models;
namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface ITokens
    {
        Task<List<Token>> GetTokens();
        Task<bool> PostTokens(Token tokens);
        Task<bool> PutTokens(Token tokens);
        Task<bool> DeleteTokens(Token tokens);
    }
}
