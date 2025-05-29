using System.Text.Json.Serialization;
using System.Text.Json;

namespace proyectocountertexdefinitivo.Converters
{
    /// <summary>
    /// Conversor personalizado para serializar y deserializar objetos <see cref="TimeSpan"/> 
    /// en formato de cadena "hh:mm:ss" utilizando System.Text.Json.
    /// </summary>
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// Convierte una cadena JSON en un objeto <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="reader">El lector JSON que proporciona la cadena de tiempo.</param>
        /// <param name="typeToConvert">El tipo de objeto que se va a convertir.</param>
        /// <param name="options">Opciones de serialización JSON.</param>
        /// <returns>El objeto <see cref="TimeSpan"/> deserializado.</returns>
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpan.Parse(reader.GetString());
        }

        /// <summary>
        /// Escribe un objeto <see cref="TimeSpan"/> como una cadena en formato "hh:mm:ss" en JSON.
        /// </summary>
        /// <param name="writer">El escritor JSON utilizado para escribir la cadena.</param>
        /// <param name="value">El valor <see cref="TimeSpan"/> que se va a serializar.</param>
        /// <param name="options">Opciones de serialización JSON.</param>
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(@"hh\:mm\:ss"));
        }
    }
}
