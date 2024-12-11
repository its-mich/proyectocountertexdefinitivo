using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories.repositories
{
    public class EmpleadoRepository : IEmpleado
    {
        private readonly CounterTexDBContext context;
        public EmpleadoRepository(CounterTexDBContext context)
        {
            this.context = context;
        }
        public async Task<List<PerfilEmpleado>> GetEmpleado()
        {
            var data = await context.PerfilEmpleados.ToListAsync();
            return data;
        }
        public async Task<bool> PostEmpleado(PerfilEmpleado perfilEmpleado)
        {
            await context.PerfilEmpleados.AddAsync(perfilEmpleado);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PutEmpleado(PerfilEmpleado perfilEmpleado)
        {
            context.PerfilEmpleados.Update(perfilEmpleado);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteEmpleado(PerfilEmpleado perfilEmpleado)
        {
            context.PerfilEmpleados.Remove(perfilEmpleado);
            await context.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteEmpleado(int id)
        {
            throw new NotImplementedException();
        }
    }
}
