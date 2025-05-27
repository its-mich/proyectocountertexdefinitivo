using proyectocountertexdefinitivo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using proyectocountertexdefinitivo.contexto;
using Microsoft.AspNetCore.Authorization;

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

        /// <summary>
        /// Constructor del controlador de autenticación.
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación, utilizada para obtener la clave y datos del JWT.</param>
        /// <param name="context">Contexto de base de datos para acceder a los usuarios.</param>
        public AuthController(IConfiguration configuration, CounterTexDBContext context)
        {
            _configuration = configuration;
            _context = context;
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
            if (login == null || string.IsNullOrEmpty(login.Correo) || string.IsNullOrEmpty(login.Clave))
            {
                return BadRequest("Petición inválida");
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == login.Correo);

            if (usuario == null)
                return Unauthorized("Correo o contraseña incorrectos");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol ?? "SinRol")
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

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
        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Clave o contraseña del usuario.
        /// </summary>
        public string Clave { get; set; }
    }
}
