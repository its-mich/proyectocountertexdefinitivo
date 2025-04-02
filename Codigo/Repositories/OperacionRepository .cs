using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
 
    
        public class OperacionRepository : IOperacionRepository
        {
            private readonly CountertexDbContext _context;

            public OperacionRepository(CountertexDbContext context)
            {
                _context = context;
            }

        public async Task<List<Operacion>> GetAllAsync()
        {
            return await _context.Operaciones.ToListAsync();
        }



        public async Task<bool> AddAsync(Operacion operacion)
        {
            await _context.Operaciones.AddAsync(operacion);
            await _context.SaveChangesAsync();
            return true;
        }

        //public async Task<Operacion> GetByIdAsync(int id)
        //{
        //    return await _context.Operaciones.FindAsync(id);
        //}

        public async Task<Operacion> GetByIdAsync(int id)
        {
            return await _context.Operaciones.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Operacion operacion)
        {
            _context.Operaciones.Update(operacion);
            await _context.SaveChangesAsync();
            return true; // Asegúrate de que devuelva un bool si la interfaz lo requiere
        }

        public async Task<bool> DeleteAsync(int id)
            {
                var entity = await _context.Operaciones.FindAsync(id);
                
                if (entity == null) 
                return false;
                _context.Operaciones.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
            }

        }
    
}
