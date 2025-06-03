using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        // GET: api/Usuarios
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

        // GET: api/Usuarios/5
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

        // PUT: api/Usuarios/5
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

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult> PostUsuario(UsuarioCreateDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si ya existe un correo igual
            var correoExistente = await _context.Usuarios
                .AnyAsync(u => u.Correo == usuarioDTO.Correo);

            if (correoExistente)
            {
                return BadRequest(new { mensaje = "El correo ya está registrado." });
            }

            var documentoExistente = await _context.Usuarios
                .AnyAsync(u => u.Documento == usuarioDTO.Documento);

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

            // DELETE: api/Usuarios/5
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

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}