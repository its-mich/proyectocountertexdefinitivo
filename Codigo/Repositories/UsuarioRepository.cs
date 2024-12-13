using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    public class UsuarioRepository : IUsuarios
    {
        private readonly CounterTexDBContext context;
        public UsuarioRepository(CounterTexDBContext context)
        {
            this.context = context;
        }
        public async Task<List<Usuarios>> GetUsuarios()
        {
            var data = await context.Usuarios.ToListAsync();
            return data;
        }
        public async Task<bool> PostUsuarios(Usuarios usuario)
        {
            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PutUsuarios(Usuarios usuario)
        {
            context.Usuarios.Update(usuario);
            await context.SaveChangesAsync();
            return true;
        }
      

        public async Task<bool> DeleteUsuarios(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id); // Usar 'context' en lugar de '_context'
            if (usuario == null) return false; // Si no existe, devolver 'false'

            context.Usuarios.Remove(usuario); // Usar 'context'
            await context.SaveChangesAsync(); // Corregir 'SaveAsync' por 'SaveChangesAsync'
            return true;
        }


    }
}
