using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace proyectocountertexdefinitivo.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de entidades <see cref="Usuario"/> en la base de datos.
    /// Implementa operaciones CRUD usando Entity Framework.
    /// </summary>
    public class UsuarioRepository : IUsuarios
    {
        /// <summary>
        /// Contexto de base de datos utilizado para acceder a las entidades.
        /// </summary>
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia de <see cref="CounterTexDBContext"/>.</param>
        public UsuarioRepository(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados en la base de datos.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="Usuario"/>.</returns>
        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .Select(u => new Usuario
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Correo = u.Correo,
                    Documento = u.Documento,
                    RolId = u.RolId,
                    RolNombre = u.Rol != null ? u.Rol.Nombre : "Sin rol",
                    Edad = u.Edad,
                    Telefono = u.Telefono
                })
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un usuario por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del usuario.</param>
        /// <returns>El objeto <see cref="Usuario"/> si se encuentra; de lo contrario, null.</returns>
        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetUsuarioByCorreoAsync(string correo)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<bool> GuardarTokenRecuperacionAsync(int usuarioId, string token)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) return false;

            usuario.TokenRecuperacion = token;
            usuario.TokenExpiracion = DateTime.UtcNow.AddMinutes(15);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuario?> GetUsuarioPorTokenAsync(string token)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.TokenRecuperacion == token && u.TokenExpiracion > DateTime.UtcNow);
        }

        public async Task<bool> LimpiarTokenRecuperacionAsync(int usuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) return false;

            usuario.TokenRecuperacion = null;
            usuario.TokenExpiracion = null;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EnviarCorreo(string destino, string codigo)
        {
            try
            {
                var remitente = "tucorreo@example.com"; // Cambia esto
                var asunto = "Código de recuperación de contraseña";

                var html = $@"
<!DOCTYPE html>
<html lang='es'>
<head>
  <meta charset='UTF-8' />
  <title>Código de recuperación</title>
  <style>
    body {{
      font-family: 'Segoe UI', sans-serif;
      background-color: #f5f5f5;
      margin: 0;
      padding: 0;
    }}
    .container {{
      max-width: 500px;
      margin: 40px auto;
      background-color: white;
      border-radius: 12px;
      box-shadow: 0 4px 12px rgba(0,0,0,0.1);
      overflow: hidden;
    }}
    .header {{
      background-color: #2c3e50;
      color: white;
      text-align: center;
      padding: 20px;
    }}
    .header img {{
      width: 100px;
      margin-bottom: 10px;
    }}
    .content {{
      padding: 30px;
      color: #333;
    }}
    .content h2 {{
      color: #2c3e50;
    }}
    .code-box {{
      font-size: 28px;
      font-weight: bold;
      background-color: #ecf0f1;
      padding: 15px;
      text-align: center;
      letter-spacing: 6px;
      border-radius: 6px;
      margin: 20px 0;
      color: #2c3e50;
    }}
    .footer {{
      text-align: center;
      font-size: 12px;
      color: #888;
      padding: 20px;
      background-color: #f9f9f9;
    }}
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>
      <img src='https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Logo_OpenAI.svg/320px-Logo_OpenAI.svg.png' alt='Logo' />
      <h1>Recuperación de Contraseña</h1>
    </div>
    <div class='content'>
      <h2>Hola,</h2>
      <p>Hemos recibido una solicitud para restablecer tu contraseña. Usa el siguiente código para continuar:</p>
      <div class='code-box'>{codigo}</div>
      <p>Este código es válido por 15 minutos. Si no solicitaste este cambio, puedes ignorar este mensaje.</p>
    </div>
    <div class='footer'>
      © 2025 motorX. Todos los derechos reservados.
    </div>
  </div>
</body>
</html>";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("motorX", remitente));
                message.To.Add(new MailboxAddress("", destino));
                message.Subject = asunto;

                var builder = new BodyBuilder
                {
                    HtmlBody = html
                };

                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.example.com", 587, false); // Cambia host y puerto
                await client.AuthenticateAsync(remitente, "TU-CONTRASEÑA"); // Cambia esto
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Instancia de <see cref="Usuario"/> a agregar.</param>
        /// <returns>El objeto <see cref="Usuario"/> creado.</returns>

        public async Task<bool> PostUsuarios([FromBody] Usuario usuario)
        {
            try
            {
                // Validaciones previas (opcional)
                var correoExistente = await _context.Usuarios.AnyAsync(u => u.Correo == usuario.Correo);
                var documentoExistente = await _context.Usuarios.AnyAsync(u => u.Documento == usuario.Documento);

                if (correoExistente || documentoExistente)
                    return false;

                usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="usuario">Instancia de <see cref="Usuario"/> con los nuevos datos.</param>
        public async Task<bool> PutUsuarios(Usuario usuario)
        {
            var existing = await _context.Usuarios.FindAsync(usuario.Id);
            if (existing == null)
                return false;

            existing.Nombre = usuario.Nombre;
            existing.Correo = usuario.Correo;
            existing.Documento = usuario.Documento;
            existing.Telefono = usuario.Telefono;
            existing.Edad = usuario.Edad;
            existing.RolId = usuario.RolId;

            if (!string.IsNullOrEmpty(usuario.Contraseña) &&
                !BCrypt.Net.BCrypt.Verify(usuario.Contraseña, existing.Contraseña))
            {
                existing.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AsignarRol(int id, int nuevoRolId)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return false;

            usuario.RolId = nuevoRolId;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Elimina un usuario de la base de datos por su identificador.
        /// </summary>
        /// <param name="id">Identificador del usuario a eliminar.</param>
        public async Task<bool> DeleteUsuarios(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
