using proyectocountertexdefinitivo.Repositories;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.Repositories.repositories;
using MicroServiceCRUD.Repositories;

namespace proyectocountertexdefinitivo
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration _configuration)
        {
            string connectionString = "";
            connectionString = _configuration["ConnectionStrings:SQLConnectionStrings"];

            services.AddDbContext<DatabaseService>(options => options.UseSqlServer(connectionString));
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
