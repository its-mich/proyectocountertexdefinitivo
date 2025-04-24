using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Controllers;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Repositories.Interfaces;


namespace proyectocountertexdefinitivo.contexto
{
    public class CounterTexDBContext : DbContext
    {
        public CounterTexDBContext(DbContextOptions<CounterTexDBContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prenda> Prendas { get; set; }
        public DbSet<Operacion> Operaciones { get; set; }
        public DbSet<Produccion> Producciones { get; set; }
        public DbSet<ProduccionDetalle> ProduccionDetalle { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<MensajeChat> MensajesChat { get; set; }
        public DbSet<Contacto> Contacto { get; set; }
        public DbSet<Tokens> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Usuarios
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombres).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Apellidos).HasMaxLength(100);
                entity.Property(e => e.Documento).HasMaxLength(20).IsRequired();
                entity.HasIndex(e => e.Documento).IsUnique();
                entity.Property(e => e.Correo).HasMaxLength(100);
                entity.HasIndex(e => e.Correo).IsUnique();
                entity.Property(e => e.Contraseña).HasMaxLength(255);
                entity.Property(e => e.Rol).HasMaxLength(20);
                entity.Property(e => e.Edad);
                entity.Property(e => e.Telefono).HasMaxLength(20);
                entity.Property(e => e.OperacionId); // Definir longitud para el string en la base de datos

                // Relación con Operación (Asegurarse de que la columna OperacionId esté bien definida)
                entity.HasOne(e => e.Operacion)
                      .WithMany(o => o.Usuarios) // Relación inversa, los usuarios están relacionados con una operación
                      .HasForeignKey(e => e.OperacionId)
                      .OnDelete(DeleteBehavior.Restrict); // O el comportamiento de eliminación que prefieras
            });

            // Prendas
            modelBuilder.Entity<Prenda>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Genero).HasMaxLength(20);
                entity.Property(e => e.Color).HasMaxLength(50);
            });

            // Operaciones
            modelBuilder.Entity<Operacion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ValorUnitario).HasColumnType("decimal(10,2)");
            });

            // Producción
            modelBuilder.Entity<Produccion>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Relación con Usuario
                entity.HasOne(p => p.Usuario)
                      .WithMany(u => u.Producciones)
                      .HasForeignKey(p => p.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);  // O el comportamiento que desees

                // Relación con Prenda
                entity.HasOne(p => p.Prenda)
                      .WithMany(pr => pr.Producciones)
                      .HasForeignKey(p => p.PrendaId)
                      .OnDelete(DeleteBehavior.Restrict);  // O el comportamiento que desees
            });

            // Producción Detalle
            modelBuilder.Entity<ProduccionDetalle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Cantidad).IsRequired();

                // Relación con Producción
                entity.HasOne(e => e.Produccion)
                      .WithMany(p => p.ProduccionDetalles)
                      .HasForeignKey(e => e.ProduccionId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relación con Operación
                entity.HasOne(e => e.Operacion)
                      .WithMany(o => o.ProduccionDetalles)  // Relación con la colección correcta
                      .HasForeignKey(e => e.OperacionId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Cálculo de ValorTotal (decimales, computado)
                entity.Property(e => e.ValorTotal)
                      .HasColumnType("decimal(10,2)")
                      .HasComputedColumnSql("[Cantidad] * (SELECT ValorUnitario FROM Operaciones WHERE Operaciones.Id = OperacionId)", stored: true);
            });


            // Horarios
            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).HasColumnType("date");
                entity.Property(e => e.HoraEntrada).HasColumnType("time");
                entity.Property(e => e.HoraSalida).HasColumnType("time");
                entity.HasOne<Usuario>().WithMany().HasForeignKey(e => e.UsuarioId);
            });

            // Metas
            modelBuilder.Entity<Meta>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Fecha).HasColumnType("date");
                entity.Property(e => e.MetaCorte);
                entity.Property(e => e.ProduccionReal);

                entity.Property(e => e.FechaHora).HasColumnType("datetime");
                entity.Property(e => e.Mensaje).HasMaxLength(500);  // Ajusta según necesites

                // Relación con Usuario (UsuarioId)
                entity.HasOne(e => e.Usuario)
                      .WithMany() // Si no necesitas la navegación inversa
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Remitente
                entity.HasOne(e => e.Remitente)
                      .WithMany()  // No se necesita navegación inversa, pero si se necesita, puedes agregar una colección en Usuario
                      .HasForeignKey(e => e.RemitenteId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Destinatario
                entity.HasOne(e => e.Destinatario)
                      .WithMany()  // Similar, no es necesario una colección en Usuario si no la quieres
                      .HasForeignKey(e => e.DestinatarioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Mensajes Chat
            modelBuilder.Entity<MensajeChat>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FechaHora).HasColumnType("datetime");

                entity.Property(e => e.Mensaje).IsRequired();

                entity.HasOne(e => e.Remitente)
                      .WithMany(u => u.MensajesEnviados)
                      .HasForeignKey(e => e.RemitenteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Destinatario)
                      .WithMany(u => u.MensajesRecibidos)
                      .HasForeignKey(e => e.DestinatarioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Contacto
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NombreCompleto).HasMaxLength(100);
                entity.Property(e => e.Telefono).HasMaxLength(20);
                entity.Property(e => e.Correo).HasMaxLength(100);
                entity.Property(e => e.Observacion);
            });
        }
    }
}
