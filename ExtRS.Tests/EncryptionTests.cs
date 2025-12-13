using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sonrai.ExtRS.UnitTests
{
    [TestClass]
    public class EncryptionTests
    {
        [TestMethod]
        public void EncryptUrlSucceeds()
        {
            Assert.IsTrue(EncryptionService.Encrypt("some clear text", "secr3tk3y") == "nNVA3kA4w+Imz4fyhK7/qsF7IUSLMZ/bsa42vAPkFPk=");
        }

        [TestMethod]
        public void EncryptUrlFails()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => EncryptionService.Encrypt(null!, null!));
        }

        [TestMethod]
        public void DecryptUrlSucceeds()
        {
            Assert.IsTrue(EncryptionService.Decrypt("nNVA3kA4w+Imz4fyhK7/qsF7IUSLMZ/bsa42vAPkFPk=", "secr3tk3y") == "some clear text");
        }

        [TestMethod]
        public void DecryptUrlFails()
        {
            Assert.ThrowsExactly<NullReferenceException>(() => EncryptionService.Decrypt(null!, null!));
        }
    }
}
