using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroServiceCRUD.Repositories
{
    public class DatabaseService : DbContext
    {
        public DatabaseService(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuarios> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfuguration(modelBuilder);
        }

        private void EntityConfuguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>().ToTable("Usuarios");
            modelBuilder.Entity<Usuarios>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Usuarios>().Property(u => u.IdUsuario).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Usuarios>().Property(u => u.NombreUsuario).HasColumnName("Nombre");
        }








        public async Task<bool> SaveAsync()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
