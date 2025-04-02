using Azure;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.contexto
{
    public class CountertexDbContext : DbContext
    {
        public CountertexDbContext(DbContextOptions<CountertexDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Operacion> Operaciones { get; set; }
        public DbSet<PerfilEmpleado> PerfilEmpleados { get; set; }
        public DbSet<OperacionesEmpleado> OperacionesEmpleados { get; set; }
        public DbSet<PerfilAdministrador> PerfilAdministradores { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<Satelite> Satelites { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones
            modelBuilder.Entity<OperacionesEmpleado>()
                .HasOne(o => o.Empleado)
                .WithMany(e => e.OperacionesRealizadas)
                .HasForeignKey(o => o.IdEmpleado);

            modelBuilder.Entity<OperacionesEmpleado>()
                .HasOne(o => o.Operacion)
                .WithMany(op => op.OperacionEmpleados)
                .HasForeignKey(o => o.IdOperacion);

            modelBuilder.Entity<PerfilEmpleado>()
                .HasOne(e => e.Usuario)
                .WithMany(u => u.Empleados)
                .HasForeignKey(e => e.IdUsuario);

            modelBuilder.Entity<PerfilAdministrador>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Administradores)
                .HasForeignKey(a => a.IdUsuario);

            modelBuilder.Entity<Satelite>()
                .HasOne(s => s.Usuario)
                .WithMany(u => u.Satelites)
                .HasForeignKey(s => s.IdUsuario);
        }
    }
}