using proyectocountertexdefinitivo.Repositories;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Repositories.repositories;
using proyectocountertexdefinitivo.contexto;


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

            // Registro de interfaces y repositorios
            services.AddScoped<IAdministrador, AdministradorRepository>();
            services.AddScoped<IEmpleado, EmpleadoRepository>();
            services.AddScoped<IRegistro, RegistroRepository>();
            services.AddScoped<ISatelite, SateliteRepository>();
            services.AddScoped<IUsuarios, UsuarioRepository>();
            services.AddScoped<ITokens, TokensRepository>();
            services.AddScoped<IProvedor, ProveedorRepository>();

            return services;
        }
    }

}
