using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{
    //public interface IOperacionRepository
    //{
    //    Task AddAsync(Operacion operacion);
    //    Task<bool> DeleteAsync(int id);
    //    Task<IEnumerable<Operacion>> GetAllAsync();
    //    Task<ActionResult<Operacion>> GetByIdAsync(int id);
    //    Task UpdateAsync(Operacion operacion);

        public interface IOperacionRepository
        {
            Task<List<Operacion>> GetAllAsync();
            Task<Operacion> GetByIdAsync(int id);
            Task<bool> AddAsync(Operacion operacion);
            Task<bool> UpdateAsync(Operacion operacion);
            Task<bool> DeleteAsync(int id);
        }
    //}
}
