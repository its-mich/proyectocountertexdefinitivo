using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using proyectocountertexdefinitivo.contexto;
using Microsoft.EntityFrameworkCore;

namespace proyectocountertexdefinitivo.Controllers
{
    //using Microsoft.AspNetCore.Mvc;
    //using Microsoft.EntityFrameworkCore;
    //using CounterTex.Models;
    //using System.Linq;
    //using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly CounterTexDBContext _context;

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
                    Nombres = u.Nombres,
                    Apellidos = u.Apellidos,
                    Documento = u.Documento,
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
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Documento = usuario.Documento,
                Correo = usuario.Correo,
                Contraseña = usuario.Contraseña,
                Rol = usuario.Rol,
                Id = usuario.Id,
            };

            return usuarioDTO;
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

            usuario.Nombres = usuarioDTO.Nombres;
            usuario.Apellidos = usuarioDTO.Apellidos;
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
        public async Task<ActionResult<UsuarioCreateDTO>> PostUsuario(UsuarioCreateDTO usuarioDTO)
        {
            // Verificar si ya existe un correo igual
            var correoExistente = await _context.Usuarios
                .AnyAsync(u => u.Correo == usuarioDTO.Correo);

            if (correoExistente)
            {
                return BadRequest(new { mensaje = "El correo ya está registrado." });
            }

            var usuario = new Usuario
            {
                Nombres = usuarioDTO.Nombres,
                Apellidos = usuarioDTO.Apellidos,
                Documento = usuarioDTO.Documento,
                Correo = usuarioDTO.Correo,
                Contraseña = usuarioDTO.Contraseña,
                Rol = "Empleado" // por defecto, o como desees
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuarioDTO);
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