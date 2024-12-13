using proyectocountertexdefinitivo.Models;

using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using proyectocountertexdefinitivo.contexto;

namespace proyectocountertexdefinitivo.Repositories
{
    public class SateliteRepository : ISatelite
    {
        private readonly CounterTexDBContext context;
        public SateliteRepository(CounterTexDBContext context)
        {
            this.context = context;
        }
        public async Task<List<Satelite>> GetSatelite()
        {
            var data = await context.Satelites.ToListAsync();
            return data;
        }
        public async Task<bool> PostSatelite(Satelite satelite)
        {
            await context.Satelites.AddAsync(satelite);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PutSatelite(Satelite satelite)
        {
            context.Satelites.Update(satelite);
            await context.SaveChangesAsync();
            return true;
        }
     


    
        public async Task<bool> DeleteSatelite(int id)
        {
            var satelite = await context.Satelites.FindAsync(id); // Usar 'context' en lugar de '_context'
            if (satelite == null) return false; // Si no existe, devolver 'false'

            context.Satelites.Remove(satelite); // Usar 'context'
            await context.SaveChangesAsync(); // Corregir 'SaveAsync' por 'SaveChangesAsync'
            return true;
        }


    }
}
