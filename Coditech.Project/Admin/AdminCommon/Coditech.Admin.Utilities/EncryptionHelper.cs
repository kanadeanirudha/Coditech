using System.Security.Cryptography;
using System.Text;

namespace Coditech.Admin.Utilities
{
    public static class EncryptionHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes(CoditechAdminSettings.EncryptionKey);
        private static readonly byte[] IV = Encoding.UTF8.GetBytes(CoditechAdminSettings.EncryptionIV);

        public static string Encrypt(string plainText)
        {
            // If encryption Is disable , then return plain text
            if (!CoditechAdminSettings.IsURLEncrypted)
                return plainText;

            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
                sw.Write(plainText);

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText)
        {
            //If encryption Is disable , then return plain text
            if (!CoditechAdminSettings.IsURLEncrypted)
                return cipherText;

            var buffer = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
