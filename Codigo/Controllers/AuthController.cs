using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador responsable de la autenticación de usuarios y generación de tokens JWT.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly CounterTexDBContext _context;
        private readonly IUsuarios _usuariosRepository;
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// Constructor del controlador de autenticación.
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación, utilizada para obtener la clave y datos del JWT.</param>
        /// <param name="context">Contexto de base de datos para acceder a los usuarios.</param>
        public AuthController(IConfiguration configuration, CounterTexDBContext context, IUsuarios usuariosRepository, ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _context = context;
            _usuariosRepository = usuariosRepository;
            _logger = logger;
        }

        /// <summary>
        /// Autentica al usuario y genera un token JWT si las credenciales son válidas.
        /// </summary>
        /// <param name="login">Objeto que contiene el correo y la clave del usuario.</param>
        /// <returns>
        /// Un objeto con el token JWT, el rol del usuario, su ID y su nombre si la autenticación es exitosa;
        /// de lo contrario, retorna un error correspondiente.
        /// </returns>
        /// <response code="200">Autenticación exitosa. Devuelve los datos del usuario y el token.</response>
        /// <response code="400">La petición está incompleta o mal formada.</response>
        /// <response code="401">Credenciales incorrectas.</response>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Correo) || string.IsNullOrEmpty(login.Contraseña))
            {
                return BadRequest("Petición inválida");
            }

            var usuario = _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.Correo == login.Correo);

            // Verificar credenciales
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Contraseña, usuario.Contraseña))
            {
                return Unauthorized("Correo o contraseña incorrectos");
            }

            // Asegura que el Rol esté asignado
            var rolNombre = usuario.Rol?.Nombre ?? "SinRol";
            if (usuario.Rol == null)
            {
                // Puedes loguearlo si deseas
                _logger?.LogWarning($"Usuario con ID {usuario.Id} no tiene un rol asignado.");
            }

            // Crear claims para el JWT
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Correo),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, rolNombre)
            };

            // Clave secreta y firma
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            // Configurar el token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Respuesta con el token y datos básicos del usuario
            return Ok(new
            {
                Token = tokenString,
                Rol = usuario.Rol,
                Id = usuario.Id,
                Nombres = usuario.Nombre
            });
        }
    }

    /// <summary>
    /// Modelo que representa las credenciales del usuario para iniciar sesión.
    /// </summary>
    public class Login
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}
