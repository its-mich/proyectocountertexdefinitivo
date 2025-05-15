using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace proyectocountertexdefinitivo.Converters
{
    public class TimeSpanSchemaFilter : ISchemaFilter
    {
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
