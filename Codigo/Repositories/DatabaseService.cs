//using proyectocountertexdefinitivo.Models;
//using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;

//namespace proyectocountertexdefinitivo.Repositories
//{
//    public class DatabaseService : DbContext
//    {
//        public DatabaseService(DbContextOptions options) : base(options)
//        {
//        }
//        public DbSet<Prenda> PerfilAdministrador { get; set; }
//        public DbSet<Produccion> PerfilEmpleado { get; set; }
//        public DbSet<ProduccionDetalle> Registro { get; set; }
//        public DbSet<Horario> Satelite { get; set; }
//        public DbSet<Operacion> Proveedor { get; set; }


//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//            EntityConfuguration(modelBuilder);
//        }

//        private void EntityConfuguration(ModelBuilder modelBuilder)
//        {

//            //Configuración Entidad Usuario
//            modelBuilder.Entity<Usuario>(tb =>
//            {
//                tb.HasKey(col => col.IdUsuario);
//                tb.Property(col => col.IdUsuario).UseIdentityColumn().ValueGeneratedOnAdd();
//                tb.Property(col => col.NombreUsuario).IsRequired().HasMaxLength(50);
//                tb.Property(col => col.Correo).HasMaxLength(50);
//                tb.Property(col => col.Clave).HasMaxLength(50);
           
//            });
//            modelBuilder.Entity<Usuario>().ToTable("Usuarios");

//            //Configuración Entidad Satélite
//            modelBuilder.Entity<Horario>(tb =>
//            {
//                tb.HasKey(col => col.SateliteId);
//                tb.Property(col => col.SateliteId).UseIdentityColumn().ValueGeneratedOnAdd();
//                tb.Property(col => col.Fabricante).IsRequired().HasMaxLength(100);
//                tb.Property(col => col.PagoPrendas).IsRequired();
//                tb.Property(col => col.Ganancias).IsRequired();
//                tb.Property(col => col.Operacion).IsRequired().HasMaxLength(100);
//                tb.Property(col => col.PagoOperacion).IsRequired();
//                tb.Property(col => col.Inventariomaquinas).IsRequired();
//                tb.Property(col => col.TipoMaquina).HasMaxLength(50);
             
//            });
//            modelBuilder.Entity<Horario>().ToTable("Satelite");

//            //Configuración Entidad PerfilAdministrador
//            modelBuilder.Entity<Prenda>(tb =>
//            {
//                tb.HasKey(col => col.IdAdministrador);
//                tb.Property(col => col.IdAdministrador).UseIdentityColumn().ValueGeneratedOnAdd();
//                tb.Property(col => col.NombreAdministrador).HasMaxLength(100);
//                tb.Property(col => col.ProduccionDiaria).IsRequired();
//                tb.Property(col => col.ProduccionMensual).IsRequired();
//                tb.Property(col => col.ControlPrendas).IsRequired();
//                tb.Property(col => col.Registro).HasMaxLength(100);
//                tb.Property(col => col.Ganancias).IsRequired();
//                tb.Property(col => col.Pagos).IsRequired();
//                tb.Property(col => col.Gastos).IsRequired();
//                tb.Property(col => col.MetaPorCorte).IsRequired();
//                tb.Property(col => col.ConsultarInformacion).IsRequired();
//                tb.Property(col => col.ControlHorarios).IsRequired();
//                tb.Property(col => col.ChatInterno).HasMaxLength(200);
//                tb.Property(col => col.Proveedor).HasMaxLength(100);
//                tb.Property(col => col.BotonAyuda).HasMaxLength(100);
           
//            });
//            modelBuilder.Entity<Prenda>().ToTable("PerfilAdministrador");

//            //Configuración Entidad PerfilEmpleado
//            modelBuilder.Entity<Produccion>(tb =>
//            {
//                tb.HasKey(col => col.IdEmpleado);
//                tb.Property(col => col.IdEmpleado).UseIdentityColumn().ValueGeneratedOnAdd();
//                tb.Property(col => col.ProduccionDiaria).IsRequired();
//                tb.Property(col => col.TipoPrenda).HasMaxLength(100);
//                tb.Property(col => col.TipoOperacion).HasMaxLength(100);
//                tb.Property(col => col.CantidadOperacion).IsRequired();
//                tb.Property(col => col.ValorOperacion).HasColumnType("decimal(18, 2)");
//                tb.Property(col => col.ConsultarInformacion).HasMaxLength(100);
//                tb.Property(col => col.ControlHorarios).IsRequired();
//                tb.Property(col => col.HoraEntrada).IsRequired();
//                tb.Property(col => col.HoraSalida).IsRequired();
//                tb.Property(col => col.MetaPorCorte).IsRequired();
//                tb.Property(col => col.BotonAyuda).HasMaxLength(100);
//                tb.Property(col => col.Estadisticas).HasMaxLength(200);
//                tb.Property(col => col.Observaciones).HasMaxLength(500);
//                tb.HasOne(col => col.Usuario).WithOne()
//                  .HasForeignKey<Produccion>(col => col.IdUsuario)
//                  .OnDelete(DeleteBehavior.Cascade); // Comportamiento de eliminación en cascada
//            });
//            modelBuilder.Entity<Produccion>().ToTable("PerfilEmpleado");

//            //Configuración Entidad Registro
//            modelBuilder.Entity<ProduccionDetalle>(tb =>
//            {
//                tb.HasKey(col => col.IdRegistro);
//                tb.Property(col => col.IdRegistro).UseIdentityColumn().ValueGeneratedOnAdd();
//                tb.Property(col => col.Nombres).IsRequired().HasMaxLength(100);
//                tb.Property(col => col.Apellidos).HasMaxLength(100);
//                tb.Property(col => col.Documento).IsRequired();
//                tb.Property(col => col.Correo).HasMaxLength(100);
//                tb.Property(col => col.Contraseña).HasMaxLength(100);
//                tb.Property(col => col.ConfirmarContraseña).HasMaxLength(100);
//                tb.Property(col => col.FechaRegistro).IsRequired();
//            });
//            modelBuilder.Entity<ProduccionDetalle>().ToTable("Registro");

//            //Configuración Entidad Proveedor
//            modelBuilder.Entity<Operacion>(tb =>
//            {
//                tb.HasKey(col => col.IdProveedor);
//                tb.Property(col => col.IdProveedor).UseIdentityColumn().ValueGeneratedOnAdd();
//                tb.Property(col => col.NombreProveedor).IsRequired().HasMaxLength(100);
//                tb.Property(col => col.PrecioPrenda).HasColumnType("decimal(18, 2)");
//                tb.Property(col => col.TipoPrenda).HasMaxLength(100).IsRequired();
//                tb.Property(col => col.Telefono).IsRequired();
//                tb.Property(col => col.NombreProveedor).HasMaxLength(150).IsRequired();
//                tb.Property(col => col.Direccion).HasMaxLength(200);
//                tb.Property(col => col.Ciudad).HasMaxLength(100);
//                tb.Property(col => col.Localidad).HasMaxLength(100);
//                tb.Property(col => col.Barrio).HasMaxLength(100);
//                tb.Property(col => col.CantidadPrendas).IsRequired();
//            });
//            modelBuilder.Entity<Operacion>().ToTable("Proveedor");
//        }



//            public async Task<bool> SaveAsync()
//            {
//                return await SaveChangesAsync() > 0;
//            }
        
//    }
//}
