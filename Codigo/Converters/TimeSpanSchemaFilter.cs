using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace proyectocountertexdefinitivo.Converters
{
    /// <summary>
    /// Filtro de esquema para representar <see cref="TimeSpan"/> como cadena en Swagger.
    /// </summary>
    public class TimeSpanSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Aplica una modificación al esquema Swagger para los tipos <see cref="TimeSpan"/>,
        /// especificando que deben representarse como cadenas con el formato "time-span".
        /// </summary>
        /// <param name="schema">El esquema de OpenAPI que se va a modificar.</param>
        /// <param name="context">Contexto que proporciona información sobre el tipo.</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(TimeSpan))
            {
                schema.Type = "string";
                schema.Format = "time-span";
                schema.Example = new OpenApiString("14:30:00");
            }
        }
    }
}
