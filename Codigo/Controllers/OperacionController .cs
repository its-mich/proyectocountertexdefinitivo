using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    public class OperacionController : Controller
    {

        [Route("api/[controller]")]
        [ApiController]
        public class OperacionController : ControllerBase
        {
            private readonly IOperacionRepository _repository;

            public OperacionController(IOperacionRepository repository)
            {
                _repository = repository;
            }

            [HttpGet]
            public async Task<IEnumerable<Operacion>> Get() => await _repository.GetAllAsync();

            [HttpGet("{id}")]
            public async Task<ActionResult<Operacion>> Get(int id)
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null) return NotFound();
                return entity;
            }

            [HttpPost]
            public async Task<IActionResult> Post(Operacion operacion)
            {
                await _repository.AddAsync(operacion);
                return CreatedAtAction(nameof(Get), new { id = operacion.IdOperacion }, operacion);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, Operacion operacion)
            {
                if (id != operacion.IdOperacion) return BadRequest();
                await _repository.UpdateAsync(operacion);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var success = await _repository.DeleteAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }
        }
    }
}

