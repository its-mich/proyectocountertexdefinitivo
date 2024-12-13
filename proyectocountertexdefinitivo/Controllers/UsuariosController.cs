using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class Usuarioscontroller : ControllerBase
    {
        private readonly IUsuarios _usuarios;

        public Usuarioscontroller(IUsuarios usuarios)
        {
            _usuarios = usuarios;
        }

        [HttpGet("GetUsuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsuarios()
        {
            var response = await _usuarios.GetUsuarios();
            return Ok(response);
        }

        [HttpPost("PostUsuarios")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostUsuarios([FromBody] Usuarios usuarios)
        {
            try
            {
                var response = await _usuarios.PostUsuarios(usuarios);
                if (response == true)
                    return Ok("bienvenido ingresaste correctamente");
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteUsuarios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUsuarios(int id)
        {
            try
            {
                var response = await _usuarios.DeleteUsuarios
                    (id);
                if (response == true)
                    return Ok("Perfil eliminado correctamente");
                else
                    return BadRequest("Error al eliminar el perfil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutUsuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUsuarios([FromBody] Usuarios usuarios)
        {
            try
            {
                var response = await _usuarios.PutUsuarios(usuarios);
                if (response == true)
                    return Ok("Perfil actualizado correctamente");
                else
                    return BadRequest("Error al actualizar el perfil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

