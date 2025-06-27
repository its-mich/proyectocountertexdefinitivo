using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    /// <summary>
    /// Controlador para operaciones relacionadas con usuarios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarios _usuarios;

        public UsuariosController(IUsuarios usuarios)
        {
            _usuarios = usuarios;
        }

        /// <summary>Obtiene todos los usuarios.</summary>
        [HttpGet("GetUsuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>Obtiene un usuario por su ID.</summary>
        [HttpGet("GetUsuariosId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>Registra un nuevo usuario.</summary>
        [HttpPost("PostUsuarios")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioCreateDTO usuarioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (usuarioDto.RolId <= 0)
                    usuarioDto.RolId = await _usuarios.ObtenerRolIdPorNombreAsync("Empleado");

                var usuario = new Usuario
                {
                    Nombre = usuarioDto.Nombre,
                    Documento = usuarioDto.Documento,
                    Correo = usuarioDto.Correo,
                    Contraseña = usuarioDto.Contraseña,
                    Edad = usuarioDto.Edad,
                    Telefono = usuarioDto.Telefono,
                    RolId = usuarioDto.RolId
                };

                var resultado = await _usuarios.PostUsuarios(usuario);

                return resultado switch
                {
                    true => Ok("Usuario registrado correctamente."),
                    false => BadRequest("Ya existe un usuario con ese correo, documento o teléfono o los datos son inválidos.")
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al registrar el usuario", error = ex.Message });
            }
        }

        /// <summary>Actualiza los datos de un usuario existente.</summary>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if (id != usuario.Id)
                    return BadRequest("El ID en la URL y el del cuerpo no coinciden.");

                var usuarioActual = await _usuarios.GetUsuarioByIdAsync(id);
                if (usuarioActual == null)
                    return NotFound("Usuario no encontrado.");

                if (!User.IsInRole("Administrador"))
                    usuario.RolId = usuarioActual.RolId;

                var response = await _usuarios.PutUsuarios(usuario);
                return response
                    ? Ok($"Usuario con ID {id} actualizado correctamente.")
                    : NotFound($"No se encontró ningún usuario con el ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el usuario", error = ex.Message });
            }
        }

        /// <summary>Asigna un nuevo rol a un usuario (solo administradores).</summary>
        [HttpPatch("AsignarRol/{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AsignarRol(int id, [FromBody] AsignarRol request)
        {
            try
            {
                var usuario = await _usuarios.AsignarRol(id, request.IdNuevoRol);
                if (usuario == null)
                    return NotFound("Usuario no encontrado.");

                return Ok($"El usuario {usuario.Nombre} ahora tiene el rol {usuario.Rol?.Nombre ?? "Desconocido"}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al asignar el rol", error = ex.Message });
            }
        }

        /// <summary>Elimina un usuario por ID (solo administradores).</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var eliminado = await _usuarios.DeleteUsuarios(id);
                return eliminado
                    ? Ok($"Usuario con ID {id} eliminado correctamente.")
                    : NotFound($"No se encontró ningún usuario con el ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al eliminar el usuario con ID {id}.", error = ex.Message });
            }
        }

        /// <summary>Solicita un código de recuperación de contraseña.</summary>
        [HttpPost("SolicitarRecuperacion")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SolicitarRecuperacion([FromBody] SolicitudRecuperacion solicitud)
        {
            try
            {
                var usuario = await _usuarios.GetUsuarioByCorreoAsync(solicitud.Correo);
                if (usuario == null)
                    return NotFound("No se encontró un usuario con ese correo.");

                var codigo = new Random().Next(100000, 999999).ToString();
                var guardado = await _usuarios.GuardarTokenRecuperacionAsync(usuario.Id, codigo);
                if (!guardado)
                    return StatusCode(500, "No se pudo guardar el código de recuperación.");

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

        /// <summary>Restablece la contraseña usando el código de recuperación.</summary>
        [HttpPost("RestablecerContrasena")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RestablecerContraseña([FromBody] RecuperarContraseña request)
        {
            try
            {
                var usuario = await _usuarios.GetUsuarioPorTokenAsync(request.Codigo);
                if (usuario == null)
                    return BadRequest("Código inválido o expirado.");

                usuario.Contraseña = request.NuevaContraseña;

                var actualizado = await _usuarios.PutUsuarios(usuario);
                if (!actualizado)
                    return StatusCode(500, "No se pudo actualizar la contraseña.");

                await _usuarios.LimpiarTokenRecuperacionAsync(usuario.Id);

                return Ok("Contraseña restablecida correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al restablecer la contraseña", error = ex.Message });
            }
        }

        /// <summary>Obtiene todos los roles disponibles.</summary>
        [HttpGet("GetRoles")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await _usuarios.GetRolesAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los roles", error = ex.Message });
            }
        }

        /// <summary>Actualiza el nombre de un usuario.</summary>
        /// <param name="id">ID del usuario.</param>
        /// <param name="model">Modelo con el nuevo nombre.</param>
        [HttpPut("ActualizarNombre/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizarNombre(int id, [FromBody] ActualizarNombreDto model)
        {
            var usuario = await _usuarios.GetByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            usuario.Nombre = model.Nombre;
            await _usuarios.UpdateAsync(usuario);

            return Ok(new { mensaje = "Nombre actualizado correctamente." });
        }
    }
}
