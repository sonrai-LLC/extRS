using System.Security.Cryptography;
using System.Text;

namespace Sonrai.ExtRS
{
    public class EncryptionService
    {
        /// <summary>
        /// Encrypts an input string with an input key.
        /// </summary>
        /// <param name="clearText">The string to encrypt.</param>
        /// <param name="enc_key">The encryption key to use for encryption of clearText.</param>
        /// <returns>
        /// An encrypted string based on the clearText input.
        /// </returns>
        public static string Encrypt(string clearText, string enc_key)
        {
            var cipherText = "";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(enc_key, new byte[14], 1000, HashAlgorithmName.SHA256);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    cipherText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return cipherText;
        }

        /// <summary>
        /// Decrypts an encrypted/cipher string with an input key.
        /// </summary>
        /// <param name="cipherText">The string to decrypt.</param>
        /// <param name="enc_key">The encryption key to use for decryption of cipherText.</param>
        /// <returns>
        /// An decrypted string based on the cipherText input.
        /// </returns>
        public static string Decrypt(string cipherText, string enc_key)
        {
            var clearText = "";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(enc_key, new byte[14], 1000, HashAlgorithmName.SHA256);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }

                    clearText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return clearText;
        }
    }
}
