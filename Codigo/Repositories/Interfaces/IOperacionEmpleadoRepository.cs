using Microsoft.AspNetCore.Mvc;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.Repositories.Interfaces
{ 
    

        public interface IOperacionEmpleadoRepository
        {
            Task<List<OperacionesEmpleado>> GetAllAsync();
            Task<OperacionesEmpleado> GetByIdAsync(int id);
            Task<bool> AddAsync(OperacionesEmpleado operacionEmpleado);
            Task<bool> UpdateAsync(OperacionesEmpleado operacionEmpleado);
            Task<bool> DeleteAsync(int id);
        }
}











//{
//    public interface IOperacionEmpleadoRepository
//    {
//        Task AddAsync(OperacionEmpleado operacionEmpleado);
//        Task<bool> DeleteAsync(int id);
//        Task<IEnumerable<OperacionEmpleado>> GetAllAsync();
//        Task<ActionResult<OperacionEmpleado>> GetByIdAsync(int id);
//        Task UpdateAsync(OperacionEmpleado operacionEmpleado);




