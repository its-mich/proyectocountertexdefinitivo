using System.Security.Cryptography;
using System.Text;

namespace proyectocountertexdefinitivo.Helpers
{
    public class AesEncryption
    {

        private static readonly string Key = "ClaveSuperSeguraDelSistema12345678"; // 32 caracteres (256 bits)
        private static readonly string IV = "VectorInicial1234"; // 16 caracteres (128 bits)

        public static string Encrypt(string plainText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);

            using MemoryStream ms = new();
            using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            using StreamWriter sw = new(cs);
            sw.Write(plainText);
            sw.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string encryptedText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);

            byte[] buffer = Convert.FromBase64String(encryptedText);
            using MemoryStream ms = new(buffer);
            using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using StreamReader sr = new(cs);
            return sr.ReadToEnd();
        }
    }
}
