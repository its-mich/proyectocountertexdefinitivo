using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.contexto;

namespace proyectocountertexdefinitivo.Controllers
{
    public class MantenimientoController : Controller
    {
        //private readonly CounterTexDBContext _context;

        //public MantenimientoController(CounterTexDBContext context)
        //{
        //    _context = context;
        //}

        //// ⚠️ Este método debe ejecutarse solo una vez para encriptar contraseñas antiguas
        //[HttpPost("EncriptarContraseñas")]
        //public IActionResult EncriptarContraseñas()
        //{
        //    var usuarios = _context.Usuarios.ToList();

        //    foreach (var user in usuarios)
        //    {
        //        if (!string.IsNullOrEmpty(user.Contraseña) && !user.Contraseña.StartsWith("$2")) // No está encriptada
        //        {
        //            user.Contraseña = BCrypt.Net.BCrypt.HashPassword(user.Contraseña);
        //        }
        //    }

        //    _context.SaveChanges();

        //    return Ok("Contraseñas actualizadas correctamente.");
        //}
    }
}
