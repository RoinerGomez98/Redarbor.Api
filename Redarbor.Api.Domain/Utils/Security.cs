using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Redarbor.Api.Domain.Utils
{
    public class Security
    {
        private readonly IConfiguration _config;
        public Security(IConfiguration config)
        {
            _config = config;
        }
        public string EncryptP(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_config["Security:Key"]!.ToString().Trim());
                aes.IV = iv;
                using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }
                array = memoryStream.ToArray();
            }
            return Convert.ToBase64String(array);
        }
    }
}
