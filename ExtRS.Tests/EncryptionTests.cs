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
            Assert.IsNotNull(EncryptionService.Encrypt("some clear text", "secr3tk3y"));
        }

        [TestMethod]
        public void EncryptUrlFails()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => EncryptionService.Encrypt(null!, null!));
        }

        [TestMethod]
        public void DecryptUrlSucceeds()
        {
            Assert.IsTrue(EncryptionService.Decrypt("HlQNwg0jh0NtayPeFTMqYUSHqQS1qvmLo8n8WVm+KoFkT0gAah8ADl3VgeMQ5RGTjEu/9peSHi1sk8w=", "secr3tk3y") == "some clear text");
        }

        [TestMethod]
        public void DecryptUrlFails()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => EncryptionService.Decrypt(null!, null!));
        }
    }
}
