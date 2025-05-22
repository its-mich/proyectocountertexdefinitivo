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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly CounterTexDBContext _context;

        public AuthController(IConfiguration configuration, CounterTexDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Correo) || string.IsNullOrEmpty(login.Clave))
            {
                return BadRequest("Invalid client request");
            }

            // Buscar al usuario
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == login.Correo);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Clave, usuario.Contraseña))
            {
                return Unauthorized("Invalid email or password");
            }

            // Clave secreta y credenciales
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Agregar claim del rol
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, usuario.Correo),
        new Claim(ClaimTypes.Role, usuario.Rol ?? "SinRol")  // por si acaso viene nulo
    };

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            // ⬅️ Aquí va la respuesta completa
            return Ok(new
            {
                Token = tokenString,
                Rol = usuario.Rol,
                Id = usuario.Id,
                Nombres = usuario.Nombre
            });
        }


    }


    public class Login
    {
        public string Correo { get; set; }
        public string Clave { get; set; }
    }
}
