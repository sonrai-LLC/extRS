using System.Security.Cryptography;
using System.Text;

namespace Sonrai.ExtRS
{
    public static class EncryptionService
    {
        private const int KeySize = 32; // 256-bit
        private const int SaltSize = 16;
        private const int NonceSize = 12;
        private const int TagSize = 16;
        private const int Iterations = 100_000;

        public static string Encrypt(string plainText, string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] nonce = RandomNumberGenerator.GetBytes(NonceSize);

            byte[] key = DeriveKey(password, salt);

            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] ciphertext = new byte[plaintextBytes.Length];
            byte[] tag = new byte[TagSize];

            using (var aes = new AesGcm(key, TagSize))
            {
                aes.Encrypt(nonce, plaintextBytes, ciphertext, tag);
            }

            using (var ms = new MemoryStream())
            {
                ms.Write(salt);
                ms.Write(nonce);
                ms.Write(tag);
                ms.Write(ciphertext);

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string Decrypt(string encryptedData, string password)
        {
            byte[] fullData = Convert.FromBase64String(encryptedData);

            using (var ms = new MemoryStream(fullData))
            {
                byte[] salt = ReadBytes(ms, SaltSize);
                byte[] nonce = ReadBytes(ms, NonceSize);
                byte[] tag = ReadBytes(ms, TagSize);
                byte[] ciphertext = ReadBytes(ms, (int)(ms.Length - ms.Position));

                byte[] key = DeriveKey(password, salt);
                byte[] plaintext = new byte[ciphertext.Length];

                using (var aes = new AesGcm(key, TagSize))
                {
                    aes.Decrypt(nonce, ciphertext, tag, plaintext);
                }

                return Encoding.UTF8.GetString(plaintext);
            }
        }

        private static byte[] DeriveKey(string password, byte[] salt)
        {
            return Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
        }

        private static byte[] ReadBytes(Stream stream, int count)
        {
            byte[] buffer = new byte[count];
            int read = stream.Read(buffer, 0, count);

            if (read != count)
                throw new CryptographicException("Invalid encrypted data.");

            return buffer;
        }
    }
}
