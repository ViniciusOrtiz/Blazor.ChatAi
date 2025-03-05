using Application.Contracts.Services;
using Application.Models.Dtos.SecurityService;
using System.Security.Cryptography;
using Application.Contracts.Settings;

namespace Infrastructure.Services
{
    public class SecurityService: ISecurityService
    {
        private readonly IAppSettings _appSettings;

        public SecurityService(
            IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        
        public SecurityKeyIvOutputDto GenerateKeyAndIV()
        {
            using var aes = Aes.Create();

            aes.KeySize = 256;
            aes.GenerateKey();
            aes.GenerateIV();

            var keyBase64 = Convert.ToBase64String(aes.Key);
            var ivBase64 = Convert.ToBase64String(aes.IV);

            return new SecurityKeyIvOutputDto
            {
                Key = keyBase64,
                Iv = ivBase64,
            };
        }

        public string EncryptText(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(_appSettings.SecuritySettings.Key);
            aes.IV = Convert.FromBase64String(_appSettings.SecuritySettings.Iv);

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cs))
            {
                writer.Write(plainText);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptText(string encryptedText)
        {
            using var aes = Aes.Create();
            aes.Padding = PaddingMode.PKCS7; // Garantir padding correto
            aes.Key = Convert.FromBase64String(_appSettings.SecuritySettings.Key);
            aes.IV = Convert.FromBase64String(_appSettings.SecuritySettings.Iv);

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(Convert.FromBase64String(encryptedText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);
            return reader.ReadToEnd();
        }

        public Stream EncryptStream(Stream inputStream)
        {
            if (inputStream == null || inputStream.Length == 0)
                throw new ArgumentException("Stream is not valid", nameof(inputStream));

            var outputStream = new MemoryStream();

            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(_appSettings.SecuritySettings.Key);
                aes.IV = Convert.FromBase64String(_appSettings.SecuritySettings.Iv);

                using (var cryptoStream = new CryptoStream(outputStream, aes.CreateEncryptor(), CryptoStreamMode.Write, leaveOpen: true))
                {
                    inputStream.CopyTo(cryptoStream);
                    cryptoStream.FlushFinalBlock();
                }
            }

            outputStream.Position = 0;

            return outputStream;
        }

        public Stream DecryptStream(Stream inputStream)
        {
            var outputStream = new MemoryStream();

            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(_appSettings.SecuritySettings.Key);
                aes.IV = Convert.FromBase64String(_appSettings.SecuritySettings.Iv);

                using var cryptoStream = new CryptoStream(inputStream, aes.CreateDecryptor(), CryptoStreamMode.Read, leaveOpen: true);
                cryptoStream.CopyTo(outputStream);
            }

            outputStream.Position = 0;
            return outputStream;
        }
    }
}
