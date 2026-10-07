using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Sonrai.ExtRS.UnitTests
{
    [TestClass]
    public class EncryptionTests
    {
        [TestMethod]
        public void EncrypAesDecryptAesWithDifferentKeysFails()
        {
            Assert.IsTrue(EncryptionService.EncryptAes("some clear text", "secr3tk3y!!") == "LK5phnfsTydwxwkPUKYNnL4MaUDpzpQraLcURIciPBM=");
            Assert.ThrowsExactly<CryptographicException>(() => EncryptionService.DecryptAes("LK5phnfsTydwxwkPUKYNnL4MaUDpzpQraLcURIciPBM=", "secr3tk3y??"));
        }

        [TestMethod]
        public void DecryptAesRfc2898DeriveBytesWithoutEscapingSpacesFails()
        {
            Assert.ThrowsExactly<FormatException>(() => DecryptAesRfc2898DeriveBytesWithoutEscapingSpaces("nNVA3kA4w+Imz4fyhK7 qsF7IUSLMZ/ bsa42vAPkFPk=", "secr3tk3y"));
        }

        [TestMethod]
        public void EncrypAesSucceeds()
        {
            Assert.IsTrue(EncryptionService.EncryptAes("some clear text", "secr3tk3y") == "nNVA3kA4w+Imz4fyhK7/qsF7IUSLMZ/bsa42vAPkFPk=");
        }

        [TestMethod]
        public void EncryptAesFails()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => EncryptionService.EncryptAes(null!, null!));
        }

        [TestMethod]
        public void DecryptAesSucceeds()
        {
            Assert.IsTrue(EncryptionService.DecryptAes("nNVA3kA4w+Imz4fyhK7/qsF7IUSLMZ/bsa42vAPkFPk=", "secr3tk3y") == "some clear text");
        }

        [TestMethod]
        public void DecryptAesUrlFails()
        {
            Assert.ThrowsExactly<NullReferenceException>(() => EncryptionService.DecryptAes(null!, null!));
        }

        [TestMethod]
        public void EncrypAesGcmtUrlSucceeds()
        {
            Assert.IsNotNull(EncryptionService.EncryptAesGcm("some clear text", "secr3tk3y"));
        }

        [TestMethod]
        public void EncryptAesGcmFails()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => EncryptionService.EncryptAesGcm(null!, null!));
        }

        [TestMethod]
        public void DecryptAesGcmSucceeds()
        {
            Assert.IsTrue(EncryptionService.DecryptAesGcm("HlQNwg0jh0NtayPeFTMqYUSHqQS1qvmLo8n8WVm+KoFkT0gAah8ADl3VgeMQ5RGTjEu/9peSHi1sk8w=", "secr3tk3y") == "some clear text");
        }

        [TestMethod]
        public void DecryptAesGcmFails()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => EncryptionService.DecryptAesGcm(null!, null!));
        }

        public static string DecryptAesRfc2898DeriveBytesWithoutEscapingSpaces(string cipherText, string encryptionKey)
        {
            var clearText = "";
            //cipherText = cipherText.Replace(" ", "+"); // NOTE THE FAILURE TO ESCAPE SPACES.. this will cause CryptographicException "invalid padding" Exception...
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            try
            {
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[14], 1000, HashAlgorithmName.SHA256);
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }

                        clearText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return clearText;
        }
    }
}
