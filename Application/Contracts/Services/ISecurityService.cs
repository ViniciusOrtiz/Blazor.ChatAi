using Application.Models.Dtos.SecurityService;
using System.IO;

namespace Application.Contracts.Services
{
    public interface ISecurityService
    {
        SecurityKeyIvOutputDto GenerateKeyAndIV();
        string EncryptText(string plainText);
        string DecryptText(string encryptedText);
        Stream EncryptStream(Stream inputStream);
        Stream DecryptStream(Stream inputStream);
        byte[] EncryptBytes(byte[] plainBytes);
        byte[] DecryptBytes(byte[] encryptedBytes);
    }
}