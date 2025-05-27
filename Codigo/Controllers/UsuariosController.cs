using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.contexto;
using Microsoft.EntityFrameworkCore;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar usuarios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Contexto de base de datos.</param>
        public UsuariosController(CounterTexDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los usuarios en formato DTO.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioCreateDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new UsuarioCreateDTO
                {
                    Nombre = u.Nombre,
                    Correo = u.Correo,
                    Contraseña = u.Contraseña,
                    Rol = u.Rol,
                    Id = u.Id,
                }).ToListAsync();

            return usuarios;
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>Usuario en formato DTO.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioCreateDTO>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDTO = new UsuarioCreateDTO
            {
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Documento = usuario.Documento,
                Contraseña = usuario.Contraseña,
                Rol = usuario.Rol,
                Id = usuario.Id,
            };

            return Ok(usuarioDTO);
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="id">ID del usuario a actualizar.</param>
        /// <param name="usuarioDTO">Datos actualizados del usuario.</param>
        /// <returns>NoContent si la actualización es exitosa.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioCreateDTO usuarioDTO)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Nombre = usuarioDTO.Nombre;
            usuario.Documento = usuarioDTO.Documento;
            usuario.Correo = usuarioDTO.Correo;
            usuario.Contraseña = usuarioDTO.Contraseña;
            usuario.Rol = usuarioDTO.Rol;
            usuario.Id = id;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Crea un nuevo usuario, validando correo y documento únicos, y cifrando la contraseña.
        /// </summary>
        /// <param name="usuarioDTO">Datos del usuario a crear.</param>
        /// <returns>Usuario creado con sus datos públicos.</returns>
        [HttpPost]
        public async Task<ActionResult> PostUsuario(UsuarioCreateDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var correoExistente = await _context.Usuarios.AnyAsync(u => u.Correo == usuarioDTO.Correo);
            if (correoExistente)
            {
                return BadRequest(new { mensaje = "El correo ya está registrado." });
            }

            var documentoExistente = await _context.Usuarios.AnyAsync(u => u.Documento == usuarioDTO.Documento);
            if (documentoExistente)
            {
                return BadRequest(new { mensaje = "El documento ya está registrado." });
            }

            var usuario = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                Documento = usuarioDTO.Documento,
                Correo = usuarioDTO.Correo,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Contraseña),
                Rol = usuarioDTO.Rol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, new
            {
                usuario.Id,
                usuario.Nombre,
                usuario.Documento,
                usuario.Correo,
                usuario.Rol
            });
        }

        /// <summary>
        /// Elimina un usuario por ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>NoContent si la eliminación es exitosa.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica si un usuario existe por ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>True si existe, False si no.</returns>
        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
