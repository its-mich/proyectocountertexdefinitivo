using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.contexto;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace proyectocountertexdefinitivo.Controllers
{
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
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            // Si la contraseña ha sido cambiada, la encriptamos antes de guardarla
            if (!string.IsNullOrEmpty(usuario.Contraseña))
            {
                usuario.Contraseña = AesEncryption.Encrypt(usuario.Contraseña);
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios (Registro)
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            // Encriptar la contraseña antes de guardar
            usuario.Contraseña = AesEncryption.Encrypt(usuario.Contraseña);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // POST: api/Usuarios/login (Login)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario login)
        {
            // Buscamos al usuario por email
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == login.Contraseña);

            if (usuario == null)
            {
                return Unauthorized("Usuario no encontrado");
            }

            // Desencriptamos la contraseña almacenada en la base de datos
            string contraseñaDesencriptada = AesEncryption.Decrypt(usuario.Contraseña);

            // Comparamos la contraseña desencriptada con la proporcionada por el usuario
            if (contraseñaDesencriptada != login.Contraseña)
            {
                return Unauthorized("Contraseña incorrecta");
            }

            // Si la contraseña es correcta, retornamos los datos del usuario (puedes agregar un JWT aquí si lo necesitas)
            return Ok(new
            {
                usuario.Id,
                usuario.Nombres,
                usuario.Correo
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
