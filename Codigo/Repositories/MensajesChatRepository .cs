using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    /// <summary>
    /// Repositorio para gestionar operaciones CRUD relacionadas con mensajes de chat.
    /// </summary>
    public class MensajesChatRepository : IMensajesChat
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de la base de datos.</param>
        public MensajesChatRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los mensajes de chat.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="MensajeChat"/>.</returns>
        public async Task<IEnumerable<MensajeChat>> ObtenerTodosAsync()
        {
            return await _context.MensajesChat.ToListAsync();
        }

        /// <summary>
        /// Obtiene un mensaje de chat por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del mensaje.</param>
        /// <returns>El objeto <see cref="MensajeChat"/> correspondiente o null si no existe.</returns>
        public async Task<MensajeChat> ObtenerPorIdAsync(int id)
        {
            return await _context.MensajesChat.FindAsync(id);
        }

        /// <summary>
        /// Obtiene todos los mensajes enviados por un usuario específico.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario remitente.</param>
        /// <returns>Una lista de mensajes enviados por el usuario.</returns>
        public async Task<IEnumerable<MensajeChat>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            return await _context.MensajesChat
                .Where(m => m.RemitenteId == usuarioId)
                .ToListAsync();
        }

        /// <summary>
        /// Agrega un nuevo mensaje de chat a la base de datos.
        /// </summary>
        /// <param name="mensaje">Objeto <see cref="MensajeChat"/> a agregar.</param>
        public async Task AgregarAsync(MensajeChat mensaje)
        {
            _context.MensajesChat.Add(mensaje);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza un mensaje existente en la base de datos.
        /// </summary>
        /// <param name="mensaje">Objeto <see cref="MensajeChat"/> con los cambios.</param>
        public async Task ActualizarAsync(MensajeChat mensaje)
        {
            _context.MensajesChat.Update(mensaje);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina un mensaje de chat por su Id.
        /// </summary>
        /// <param name="id">Identificador del mensaje a eliminar.</param>
        public async Task EliminarAsync(int id)
        {
            var mensaje = await _context.MensajesChat.FindAsync(id);
            if (mensaje != null)
            {
                _context.MensajesChat.Remove(mensaje);
                await _context.SaveChangesAsync();
            }
        }
    }
}
