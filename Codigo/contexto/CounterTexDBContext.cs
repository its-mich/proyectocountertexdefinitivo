using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Models;

namespace proyectocountertexdefinitivo.contexto
{
    /// <summary>
    /// Contexto de base de datos para la aplicación CounterTex.
    /// Define las entidades y relaciones del modelo de datos.
    /// </summary>
    public class CounterTexDBContext : DbContext
    {
        /// <summary>
        /// Constructor que recibe las opciones de configuración para el contexto.
        /// </summary>
        /// <param name="options">Opciones del contexto DbContext.</param>
        public CounterTexDBContext(DbContextOptions<CounterTexDBContext> options) : base(options) { }

        /// <summary>
        /// Rol de usuarios.
        /// </summary>
        public DbSet<Rol> Roles { get; set; }
        /// <summary>
        /// DbSet de usuarios.
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }

        /// <summary>
        /// DbSet de prendas.
        /// </summary>
        public DbSet<Prenda> Prendas { get; set; }

        /// <summary>
        /// DbSet de operaciones.
        /// </summary>
        public DbSet<Operacion> Operaciones { get; set; }

        /// <summary>
        /// DbSet de producciones.
        /// </summary>
        public DbSet<Produccion> Producciones { get; set; }

        /// <summary>
        /// DbSet de detalles de producción.
        /// </summary>
        public DbSet<ProduccionDetalle> ProduccionDetalles { get; set; }

        /// <summary>
        /// DbSet de horarios.
        /// </summary>
        public DbSet<Horario> Horarios { get; set; }

        /// <summary>
        /// DbSet de metas.
        /// </summary>
        public DbSet<Meta> Metas { get; set; }

        /// <summary>
        /// DbSet de mensajes de chat.
        /// </summary>
        public DbSet<MensajeChat> MensajesChat { get; set; }

        /// <summary>
        /// DbSet de contactos.
        /// </summary>
        public DbSet<Contacto> Contactos { get; set; }

        // 👇 Agregado: DbSet para resultados de SP
        public DbSet<ProduccionMensualResumenDTO> ProduccionMensualResumen { get; set; }

        /// <summary>
        /// Configura las entidades, sus propiedades, relaciones y restricciones.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo para configurar las entidades.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la entidad Rol
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.HasIndex(e => e.Nombre)
                      .IsUnique();
            });


            // Configuración para la entidad Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id); // Clave primaria

                entity.Property(e => e.Nombre)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.Documento)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.HasIndex(e => e.Documento)
                      .IsUnique(); // Documento único

                entity.Property(e => e.Correo)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.HasIndex(e => e.Correo)
                      .IsUnique(); // Correo único

                entity.Property(e => e.Contraseña)
                      .HasMaxLength(255);

                entity.Property(e => e.Edad);

                entity.Property(e => e.Telefono)
                      .HasMaxLength(20);

                entity.HasOne(e => e.Rol)
                      .WithMany(r => r.Usuarios)
                      .HasForeignKey(e => e.RolId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para la entidad Prenda
            modelBuilder.Entity<Prenda>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Genero).HasMaxLength(20);
                entity.Property(e => e.Color).HasMaxLength(50);
            });

            // Configuración para la entidad Operacion
            modelBuilder.Entity<Operacion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ValorUnitario)
                      .HasColumnType("decimal(10,2)")
                      .HasDefaultValue(0.00m)
                      .IsRequired();
            });

            // Configuración para Produccion y sus relaciones
            modelBuilder.Entity<Produccion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).HasColumnType("date");
                entity.Property(e => e.TotalValor).HasColumnType("decimal(10,2)");

                entity.HasOne(p => p.Usuario)
                      .WithMany(u => u.Producciones)
                      .HasForeignKey(p => p.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Prenda)
                      .WithMany(pr => pr.Producciones)
                      .HasForeignKey(p => p.PrendaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para ProduccionDetalle con columna calculada
            modelBuilder.Entity<ProduccionDetalle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.ValorTotal).HasColumnType("decimal(10,2)");

                entity.HasOne(e => e.Produccion)
                      .WithMany(p => p.ProduccionDetalles)
                      .HasForeignKey(e => e.ProduccionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Operacion)
                      .WithMany(o => o.ProduccionDetalles)
                      .HasForeignKey(e => e.OperacionId)
                      .OnDelete(DeleteBehavior.Restrict);


            });

            // Configuración para Horarios
            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => e.HorarioId);
                entity.Property(e => e.HorarioId).ValueGeneratedOnAdd();
                entity.Property(e => e.Tipo).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Hora).HasColumnType("time").IsRequired();
                entity.Property(e => e.Fecha).HasColumnType("date").IsRequired();
                entity.Property(e => e.Observaciones).HasColumnType("nvarchar(max)");

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Horarios)
                      .HasForeignKey(e => e.EmpleadoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para Metas
            modelBuilder.Entity<Meta>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).HasColumnType("date");
                entity.Property(e => e.MetaCorte);
                entity.Property(e => e.ProduccionReal);
                entity.Property(e => e.FechaHora).HasColumnType("datetime");
                entity.Property(e => e.Mensaje).HasMaxLength(500);

                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Remitente)
                      .WithMany()
                      .HasForeignKey(e => e.RemitenteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Destinatario)
                      .WithMany()
                      .HasForeignKey(e => e.DestinatarioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para Mensajes Chat
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

            // Token (no persistido en la base de datos)
            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasNoKey(); // No tiene clave primaria ni tabla física
                entity.Property(e => e.TokenValue).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Rol).HasMaxLength(50);
            });

            // Configuración para Contacto
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NombreCompleto).HasMaxLength(100);
                entity.Property(e => e.Telefono).HasMaxLength(20);
                entity.Property(e => e.Correo).HasMaxLength(100);
                entity.Property(e => e.Observacion);
            });

            // 👇 Agregado: Configuración para DTO del procedimiento almacenado
            modelBuilder.Entity<ProduccionMensualResumenDTO>().HasNoKey().ToView(null);
        }

        // 👇 Método para llamar al procedimiento
        public async Task<ProduccionMensualResumenDTO?> ObtenerResumenMensualAsync(int anio, int mes)
        {
            return await this.ProduccionMensualResumen
                .FromSqlRaw("EXEC sp_ProduccionMensualResumen @Anio = {0}, @Mes = {1}", anio, mes)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
    
}
