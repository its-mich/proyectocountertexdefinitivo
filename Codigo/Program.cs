using proyectocountertexdefinitivo.Repositories;
using proyectocountertexdefinitivo.Models;
using proyectocountertexdefinitivo.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using proyectocountertexdefinitivo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using proyectocountertexdefinitivo.contexto;
using proyectocountertexdefinitivo;
using proyectocountertexdefinitivo.Repositories.repositories;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Agregar servicios externos
builder.Services.AddExternal(builder.Configuration);

// 🔌 Configurar DbContext con cadena de conexión
builder.Services.AddDbContext<CounterTexDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🧩 Inyección de dependencias
builder.Services.AddScoped<IUsuarios, UsuarioRepository>();

// 🔁 Conversión para TimeSpan en JSON (POST/GET desde Swagger)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

    });

// 🔁 También aplica para MVC (Swagger lo requiere)
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
});

// 🛡️ Swagger y Autenticación JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroServiceProyectoCounterTex", Version = "v1" });
    c.SchemaFilter<TimeSpanSchemaFilter>();

    // 🔐 Configuración del esquema JWT para Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando el esquema Bearer.  
                        <br />Escribe 'Bearer' [espacio] y luego tu token.<br />
                        Ejemplo: 'Bearer 123456abcdefg'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// 🌐 Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()   // o usa .WithOrigins("https://localhost:port") para restringir
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// 🔐 Configuración de JWT Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();


// 🧪 Middleware de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Middleware CORS debe estar antes de Autenticación
app.UseCors("AllowAll");

// 🔐 Middlewares
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// 🎯 Mapear controladores
app.MapControllers();

app.Run();