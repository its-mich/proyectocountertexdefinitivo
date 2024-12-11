using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;

using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories.repositories
{
    public class AdministradorRepository :IAdministrador
    {
        private readonly CounterTexDBContext context;
        public AdministradorRepository(CounterTexDBContext context)
        {
            this.context = context;
        }
        public async Task<List<PerfilAdministrador>> GetAdministrador()
        {
            var data = await context.PerfilAdministradores.ToListAsync();
            return data;
        }
        public async Task<bool> PostAdministrador(PerfilAdministrador perfilAdministrador)
        {
            await context.PerfilAdministradores.AddAsync(perfilAdministrador);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PutAdministrador(PerfilAdministrador perfilAdministrador)
        {
            context.PerfilAdministradores.Update(perfilAdministrador);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAdministrador(PerfilAdministrador perfilAdministrador)
        {
            context.PerfilAdministradores.Remove(perfilAdministrador);
            await context.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAdministrador(int id)
        {
            throw new NotImplementedException();
        }
    }
}
