using proyectocountertexdefinitivo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para la gestión de tokens en memoria.
    /// </summary>
    public interface ITokens
    {
        /// <summary>
        /// Obtiene todos los tokens almacenados.
        /// </summary>
        /// <returns>Lista de tokens.</returns>
        Task<List<Token>> GetTokens();

        /// <summary>
        /// Agrega un nuevo token.
        /// </summary>
        /// <param name="token">Token a agregar.</param>
        /// <returns>True si se agregó correctamente, false en caso contrario.</returns>
        Task<bool> PostTokens(Token token);

        /// <summary>
        /// Actualiza un token existente.
        /// </summary>
        /// <param name="token">Token con los datos actualizados.</param>
        /// <returns>True si se actualizó correctamente, false en caso contrario.</returns>
        Task<bool> PutTokens(Token token);

        /// <summary>
        /// Elimina un token.
        /// </summary>
        /// <param name="token">Token a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
        Task<bool> DeleteTokens(Token token);
    }
}
