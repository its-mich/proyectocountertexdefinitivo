using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace proyectocountertexdefinitivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Satelitecontroller : ControllerBase
    {
       

            private readonly ISatelite _satelite;

            public Satelitecontroller(ISatelite satelite)
            {
                _satelite = satelite;
            }

            [HttpGet("GetSatelite")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
            public async Task<IActionResult> GeGetSatelite()
            {
                var response = await _satelite.GetSatelite();
                return Ok(response);
            }

            [HttpPost("PostSatelite")]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<IActionResult> PostProveedor([FromBody] Satelite satelite)
            {
                try
                {
                    var response = await _satelite.PostSatelite(satelite);
                    if (response == true)
                        return Ok("perfil ingresado correctamente");
                    else
                        return BadRequest(response);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


        [HttpDelete("DeleteSatelite/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSatelite(int id)
        {
            try
            {
                var response = await _satelite.DeleteSatelite(id);
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


        [HttpPut("PutSatelite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSatelite([FromBody] Satelite satelite)
        {
            try
            {
                var response = await _satelite.PutSatelite(satelite);
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
