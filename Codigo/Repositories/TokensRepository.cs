using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    public class TokensRepository : ITokens
    {
        // Lista en memoria para simular almacenamiento de tokens
        private readonly List<Token> tokensList = new List<Token>();

        // Obtener todos los tokens
        public Task<List<Token>> GetTokens()
        {
            return Task.FromResult(tokensList);
        }

        // Agregar un nuevo token
        public async Task<bool> PostTokens(Token token)
        {
            tokensList.Add(token);
            return await Task.FromResult(true);
        }

        // Actualizar un token existente
        public async Task<bool> PutTokens(Token token)
        {
            var existingToken = tokensList.FirstOrDefault(t => t.TokenValue == token.TokenValue);
            if (existingToken != null)
            {
                existingToken.Rol = token.Rol;
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        // Eliminar un token
        public async Task<bool> DeleteTokens(Token token)
        {
            var existingToken = tokensList.FirstOrDefault(t => t.TokenValue == token.TokenValue);
            if (existingToken != null)
            {
                tokensList.Remove(existingToken);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}
