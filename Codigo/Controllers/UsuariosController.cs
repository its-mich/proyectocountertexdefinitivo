using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;


namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para gestionar usuarios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarios _usuarios;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Contexto de base de datos.</param>
        public UsuariosController(IUsuarios usuarios)
        {
            _usuarios = usuarios;
        }

        /// <summary>
        /// Obtiene todos los usuarios en formato DTO.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        [HttpGet("GetUsuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var response = await _usuarios.GetUsuarios();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno al obtener usuarios",
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>Usuario.</returns>
        [HttpGet("GetUsuariosId/{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _usuarios.GetUsuarioByIdAsync(id);
                if (usuario == null)
                    return NotFound("Usuario no encontrado.");
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al buscar el usuario", error = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo usuario, validando correo y documento únicos, y cifrando la contraseña.
        /// </summary>
        /// <param name="usuario">Datos del usuario a crear.</param>
        /// <returns>Usuario creado con sus datos públicos.</returns>
        [HttpPost("PostUsuarios")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize] // Requiere autenticación
        [AllowAnonymous]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            try
            {
                // Verificar si el usuario está autenticado y es administrador
                bool esAdmin = User.Identity?.IsAuthenticated == true && User.IsInRole("Administrador");

                if (!esAdmin)
                {
                    // Si no es administrador, forzamos un rol por defecto (ej. Empleado = RolId 2)
                    usuario.RolId = 2; // Id del rol "Empleado"
                }

                var response = await _usuarios.PostUsuarios(usuario);
                if (response)
                    return Ok("Usuario registrado correctamente.");
                else
                    return BadRequest("Ya existe un usuario con ese correo o documento.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al registrar el usuario", error = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <remarks>
        /// Solo los administradores pueden modificar el rol del usuario. Si el usuario no es administrador,
        /// su rol actual se mantiene sin cambios.
        /// </remarks>
        /// <param name="id">ID del usuario a actualizar (debe coincidir con el del cuerpo).</param>
        /// <param name="usuario">Datos actualizados del usuario.</param>
        /// <returns>Un mensaje indicando el resultado de la operación.</returns>
        [HttpPut("{id}")]
        [Authorize] // <- Permite a todos los autenticados
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(object))]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if (id != usuario.Id)
                    return BadRequest("El ID en la URL y el del cuerpo no coinciden.");

                var usuarioActual = await _usuarios.GetUsuarioByIdAsync(id);
                if (usuarioActual == null)
                    return NotFound("Usuario no encontrado.");

                // Si NO es admin, mantener el rol actual (no dejar que lo modifique)
                if (!User.IsInRole("Administrador"))
                {
                    usuario.RolId = usuarioActual.RolId;
                }

                var response = await _usuarios.PutUsuarios(usuario);
                if (response)
                    return Ok($"Usuario con ID {id} actualizado correctamente.");
                else
                    return NotFound($"No se encontró ningún usuario con el ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el usuario", error = ex.Message });
            }
        }

        [HttpPatch("AsignarRol/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AsignarRol(int id, [FromBody] AsignarRol request)
        {
            try
            {
                var usuario = await _usuarios.AsignarRol(id, request.IdNuevoRol);
                if (usuario == null)
                    return NotFound("Usuario no encontrado.");

                string nombreUsuario = usuario.Nombre;
                string nombreRol = usuario.Rol?.Nombre ?? "Rol no asignado";

                string mensaje = $"El usuario {nombreUsuario} ahora tiene el rol {nombreRol}.";
                return Ok(mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al asignar el rol", error = ex.Message });
            }
        }

            /// <summary>
            /// Elimina un usuario por ID.
            /// </summary>
            /// <param name="id">ID del usuario a eliminar.</param>
            /// <returns>NoContent si la eliminación es exitosa.</returns>

            [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var result = await _usuarios.DeleteUsuarios(id);
                if (result)
                    return Ok($"Usuario con ID {id} eliminado correctamente.");
                else
                    return NotFound($"No se encontró ningún usuario con el ID {id}. Verifique e intente de nuevo.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al eliminar el usuario con ID {id}.", error = ex.Message });
            }
        }

        // Solicitar recuperación de Contraseña
        [HttpPost("SolicitarRecuperacion")]
        [AllowAnonymous]
        public async Task<IActionResult> SolicitarRecuperacion([FromBody] string correo)
        {
            try
            {
                var usuario = await _usuarios.GetUsuarioByCorreoAsync(correo);
                if (usuario == null)
                    return NotFound("No se encontró un usuario con ese correo.");

                // Generar un código de 6 dígitos
                var codigo = new Random().Next(100000, 999999).ToString();

                var guardado = await _usuarios.GuardarTokenRecuperacionAsync(usuario.Id, codigo);
                if (!guardado)
                    return StatusCode(500, "No se pudo guardar el código de recuperación.");

                // Enviar correo con el código
                var enviado = await _usuarios.EnviarCorreo(usuario.Correo, codigo);
                if (!enviado)
                    return StatusCode(500, "Error al enviar el correo con el código.");

                return Ok("Código de recuperación enviado al correo.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al generar token de recuperación", error = ex.Message });
            }
        }

        // Restablecer contraseña
        [HttpPost("RestablecerContraseña")]
        [AllowAnonymous]
        public async Task<IActionResult> RestablecerContraseña([FromBody] RecuperarContraseña request)
        {
            try
            {
                var usuario = await _usuarios.GetUsuarioPorTokenAsync(request.Codigo);
                if (usuario == null)
                    return BadRequest("Código inválido o expirado.");

                usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(request.NuevaContraseña);

                var result = await _usuarios.PutUsuarios(usuario);
                if (!result)
                    return StatusCode(500, "No se pudo actualizar la contraseña.");

                await _usuarios.LimpiarTokenRecuperacionAsync(usuario.Id);

                return Ok("Contraseña restablecida correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al restablecer la contraseña", error = ex.Message });
            }
        }
    }
}
