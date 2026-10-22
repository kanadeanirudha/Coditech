using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coditech.Common.Helper
{
    public static class DataEncryptionHelper
    {
        private const int NonceSize = 12;
        private const int TagSize = 16;

        public static string DataEncrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            byte[] key = GetKey();
            byte[] nonce = RandomNumberGenerator.GetBytes(NonceSize);
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] ciphertext = new byte[plaintextBytes.Length];
            byte[] tag = new byte[TagSize];

            using (var aes = new AesGcm(key, TagSize))
            {
                aes.Encrypt(nonce, plaintextBytes, ciphertext, tag);
            }

            var result = new byte[NonceSize + TagSize + ciphertext.Length];
            Buffer.BlockCopy(nonce, 0, result, 0, NonceSize);
            Buffer.BlockCopy(tag, 0, result, NonceSize, TagSize);
            Buffer.BlockCopy(ciphertext, 0, result, NonceSize + TagSize, ciphertext.Length);

            return Convert.ToBase64String(result);
        }

        public static string DataDecrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            var buffer = Convert.FromBase64String(cipherText);
            if (buffer.Length < NonceSize + TagSize)
                throw new CryptographicException("The encrypted value is invalid.");

            byte[] nonce = buffer[..NonceSize];
            byte[] tag = buffer[NonceSize..(NonceSize + TagSize)];
            byte[] ciphertext = buffer[(NonceSize + TagSize)..];
            byte[] plaintext = new byte[ciphertext.Length];

            using (var aes = new AesGcm(GetKey(), TagSize))
            {
                aes.Decrypt(nonce, ciphertext, tag, plaintext);
            }

            return Encoding.UTF8.GetString(plaintext);
        }

        private static byte[] GetKey()
        {
            var configuredKey = CoditechDependencyResolver._staticServiceProvider?
                .GetService<IConfiguration>()?
                .GetSection("appsettings")["DataEncryptionKey"];
            if (string.IsNullOrWhiteSpace(configuredKey))
                throw new InvalidOperationException("DataEncryptionKey must be configured.");

            return SHA256.HashData(Encoding.UTF8.GetBytes(configuredKey));
        }
    }
}
