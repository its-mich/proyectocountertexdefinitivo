using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    /// <summary>
    /// Repositorio que gestiona operaciones CRUD sobre tokens en memoria.
    /// </summary>
    public class TokensRepository : ITokens
    {
        /// <summary>
        /// Lista en memoria que simula el almacenamiento de tokens.
        /// </summary>
        private readonly List<Token> tokensList = new List<Token>();

        /// <summary>
        /// Obtiene todos los tokens almacenados.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="Token"/>.</returns>
        public Task<List<Token>> GetTokens()
        {
            return Task.FromResult(tokensList);
        }

        /// <summary>
        /// Agrega un nuevo token a la lista en memoria.
        /// </summary>
        /// <param name="token">El objeto <see cref="Token"/> a agregar.</param>
        /// <returns>True si el token fue agregado exitosamente.</returns>
        public async Task<bool> PostTokens(Token token)
        {
            tokensList.Add(token);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Actualiza un token existente en la lista.
        /// </summary>
        /// <param name="token">El objeto <see cref="Token"/> con los nuevos datos.</param>
        /// <returns>True si el token fue actualizado; false si no se encontró.</returns>
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

        /// <summary>
        /// Elimina un token de la lista en memoria.
        /// </summary>
        /// <param name="token">El objeto <see cref="Token"/> a eliminar.</param>
        /// <returns>True si el token fue eliminado; false si no se encontró.</returns>
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
