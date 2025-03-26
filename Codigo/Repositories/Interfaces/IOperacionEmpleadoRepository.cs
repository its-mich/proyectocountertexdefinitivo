using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    public interface IOperacionEmpleadoRepository
    {
        Task AddAsync(OperacionEmpleado operacionEmpleado);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<OperacionEmpleado>> GetAllAsync();
        Task<ActionResult<OperacionEmpleado>> GetByIdAsync(int id);
        Task UpdateAsync(OperacionEmpleado operacionEmpleado);

        public interface IOperacionEmpleadoRepository
        {
            Task<List<OperacionEmpleado>> GetAllAsync();
            Task<OperacionEmpleado> GetByIdAsync(int id);
            Task<bool> AddAsync(OperacionEmpleado operacionEmpleado);
            Task<bool> UpdateAsync(OperacionEmpleado operacionEmpleado);
            Task<bool> DeleteAsync(int id);
        }
    }
}
