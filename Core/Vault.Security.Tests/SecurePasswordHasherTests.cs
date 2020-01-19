using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vault.Security.Tests
{
    public class SecurePasswordHasherTests
    {
        [TestCase]
        public void Password_is_valid_test()
        {
            var hashPassword = SecurePasswordHasher.Hash("test_password");

            Assert.IsTrue(SecurePasswordHasher.Verify("test_password", hashPassword));
        }

        [TestCase]
        public void Password_is_invalid_test()
        {
            var hashPassword = SecurePasswordHasher.Hash("test_password");

            Assert.IsFalse(SecurePasswordHasher.Verify("1234567", hashPassword));
        }

        [TestCase]
        public void Invalid_verify_password_test()
        {
            var fakeHash = "qwertyuiop";

            Assert.Catch<NotSupportedException>(() =>
            {
                SecurePasswordHasher.Verify("1234567", fakeHash);
            });
        }
    }
}
