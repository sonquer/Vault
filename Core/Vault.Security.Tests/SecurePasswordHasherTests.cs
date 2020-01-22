using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vault.Security.Tests
{
    public class SecurePasswordHasherTests
    {
        [TestCase]
        public void Verify_HashedPassword_IsVerifiedTrue()
        {
            var hashPassword = SecurePasswordHasher.Hash("test_password");

            Assert.IsTrue(SecurePasswordHasher.Verify("test_password", hashPassword));
        }

        [TestCase]
        public void Verify_HashedPassword_IsVerifiedFalse()
        {
            var hashPassword = SecurePasswordHasher.Hash("test_password");

            Assert.IsFalse(SecurePasswordHasher.Verify("1234567", hashPassword));
        }

        [TestCase]
        public void Verify_PlainTextPassword_NotSupportedExceptionIsThrown()
        {
            var fakeHash = "qwertyuiop";

            Assert.Catch<NotSupportedException>(() =>
            {
                SecurePasswordHasher.Verify("1234567", fakeHash);
            });
        }
    }
}
