using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;

namespace proyectocountertexdefinitivo.Repositories
{
    public class UsuarioRepository : IUsuarios
    {
        private readonly CountertexDbContext context;
        public UsuarioRepository(CountertexDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            var data = await context.Usuarios.ToListAsync();
            return data;
        }
        public async Task<bool> PostUsuarios(Usuario usuario)
        {
            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PutUsuarios(Usuario usuario)
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
