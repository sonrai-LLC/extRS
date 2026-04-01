using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sonrai.ExtRS.UnitTests
{
    [TestClass]
    public class EncryptionTests
    {
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
    }
}
