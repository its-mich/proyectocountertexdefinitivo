using proyectocountertexdefinitivo.Repositories;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using proyectocountertexdefinitivo.Repositories.repositories;
using proyectocountertexdefinitivo.contexto;
using Microsoft.EntityFrameworkCore;


namespace proyectocountertexdefinitivo
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de la cadena de conexión
            string connectionString = configuration.GetConnectionString("SQLConnectionStrings");

            // Registro del DbContext
            services.AddDbContext<CounterTexDBContext>(options =>
                options.UseSqlServer(connectionString));

            // Registro de interfaces y repositorios correctos
            services.AddScoped<IUsuarios, UsuarioRepository>();
            services.AddScoped<IPrenda, PrendaRepository>();
            services.AddScoped<IOperacion, OperacionRepository>();
            services.AddScoped<IProduccion, ProduccionRepository>();
            services.AddScoped<IProduccionDetalle, ProduccionDetalleRepository>();
            services.AddScoped<IHorario, HorarioRepository>();
            services.AddScoped<IMeta, MetaRepository>();
            services.AddScoped<IMensajesChat, MensajesChatRepository>();
            services.AddScoped<IContacto, ContactoRepository>();
            services.AddScoped<IPagoRepository, PagoRepository>();

            return services;
        }
    }
}
