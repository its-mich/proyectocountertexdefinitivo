using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Controllers
{
    public class OperacionEmpleadoController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class OperacionEmpleadoController : ControllerBase
        {
            private readonly IOperacionEmpleadoRepository _repository;

            public OperacionEmpleadoController(IOperacionEmpleadoRepository repository)
            {
                _repository = repository;
            }

            [HttpGet]
            public async Task<IEnumerable<OperacionEmpleado>> Get() => await _repository.GetAllAsync();

            [HttpGet("{id}")]
            public async Task<ActionResult<OperacionEmpleado>> Get(int id)
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null) return NotFound();
                return entity;
            }

            [HttpPost]
            public async Task<IActionResult> Post(OperacionEmpleado operacionEmpleado)
            {
                await _repository.AddAsync(operacionEmpleado);
                return CreatedAtAction(nameof(Get), new { id = operacionEmpleado.IdOperacionEmpleado }, operacionEmpleado);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, OperacionEmpleado operacionEmpleado)
            {
                if (id != operacionEmpleado.IdOperacionEmpleado) return BadRequest();
                await _repository.UpdateAsync(operacionEmpleado);
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
