using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;


namespace proyectocountertexdefinitivo.Repositories.repositories
{
    public class ProveedorRepository : IProvedor
    {
        private readonly CounterTexDBContext context;
        public ProveedorRepository(CounterTexDBContext context)
        {
            this.context = context;
        }
        public async Task<List<Proveedor>> GetProveedor()
        {
            var data = await context.Proveedores.ToListAsync();
            return data;
        }
        public async Task<bool> PostProveedor(Proveedor proveedor)
        {
            await context.Proveedores.AddAsync(proveedor);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PutProveedor(Proveedor proveedor)
        {
            context.Proveedores.Update(proveedor);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteProveedor(Proveedor proveedor)
        {
            context.Proveedores.Remove(proveedor);
            await context.SaveChangesAsync();
            return true;
        }

        // Método corregido: Elimina un empleado por su ID
        public async Task<bool> DeleteProveedor(int id)
        {
            var empleado = await context.PerfilEmpleados.FindAsync(id);
            if (empleado == null)
            {
                return false; // No se encontró el empleado con ese ID
            }
            context.PerfilEmpleados.Remove(empleado);
            await context.SaveChangesAsync();
            return true; // Se eliminó correctamente
        }
    }
}
