using System.Collections.Concurrent;

namespace proyectocountertexdefinitivo.Models
{
    public class GuardarTokenRecuperacion
    {
        // Diccionario para almacenar tokens temporalmente (correo -> token)
        public static ConcurrentDictionary<string, string> Tokens = new();
    }
}
