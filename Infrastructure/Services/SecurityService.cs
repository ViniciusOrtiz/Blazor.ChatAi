using Application.Contracts.Services;
using Application.Models.Dtos.SecurityService;
using System.Security.Cryptography;
using Application.Contracts.Settings;

namespace Infrastructure.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IAppSettings _appSettings;

        public SecurityService(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public SecurityKeyIvOutputDto GenerateKeyAndIV()
        {
            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.GenerateKey();
            aes.GenerateIV();

            return new SecurityKeyIvOutputDto
            {
                Key = Convert.ToBase64String(aes.Key),
                Iv = Convert.ToBase64String(aes.IV),
            };
        }

        public string EncryptText(string plainText)
        {
            var plainBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = EncryptBytes(plainBytes);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string DecryptText(string encryptedText)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var decryptedBytes = DecryptBytes(encryptedBytes);
            return System.Text.Encoding.UTF8.GetString(decryptedBytes);
        }

        public byte[] EncryptBytes(byte[] plainBytes)
        {
            using var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(_appSettings.SecuritySettings.Key);
            aes.IV = Convert.FromBase64String(_appSettings.SecuritySettings.Iv);

            using var encryptor = aes.CreateEncryptor();
            return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public byte[] DecryptBytes(byte[] encryptedBytes)
        {
            using var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(_appSettings.SecuritySettings.Key);
            aes.IV = Convert.FromBase64String(_appSettings.SecuritySettings.Iv);

            using var decryptor = aes.CreateDecryptor();
            return decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
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
