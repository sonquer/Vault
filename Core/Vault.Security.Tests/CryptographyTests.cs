using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Vault.Security.Tests
{
    public class CryptographyTests
    {
        [TestCase]
        public void Encrypt_and_decrypt_test()
        {
            var secret = "secret text";

            var encryptedData = Cryptography.Encrypt(Encoding.UTF8.GetBytes(secret), "password");

            var decryptedData = Cryptography.Decrypt(encryptedData, "password");

            Assert.AreEqual(secret, Encoding.UTF8.GetString(decryptedData));
        }

        [TestCase]
        public void Wrong_password_test()
        {
            var secret = "secret text";

            var encryptedData = Cryptography.Encrypt(Encoding.UTF8.GetBytes(secret), "password");

            Assert.Catch<CryptographicException>(() =>
            {
                Cryptography.Decrypt(encryptedData, "123456");
            });
        }
    }
}
