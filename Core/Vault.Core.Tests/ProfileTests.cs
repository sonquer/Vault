using NUnit.Framework;
using Vault.Core.Models;
using Vault.Security.Exceptions;

namespace Vault.Core.Tests
{
    public class ProfileTests
    {
        [TestCase]
        public void Constructor_CreatedPasswordEntity_ProfileNameIsEqual()
        {
            var profile = new Profile("test_name", "test_password", "test_recovery_code");

            Assert.AreEqual("test_name", profile.Name);
        }

        [TestCase]
        public void PasswordIsValid_CreatedPasswordEntity_PasswordIsValidTrue()
        {
            var profile = new Profile("test_name", "test_password", "test_recovery_code");

            Assert.IsTrue(profile.PasswordIsValid("test_password"));
        }

        [TestCase]
        public void PasswordIsValid_CreatedPasswordEntity_PasswordIsValidFalse()
        {
            var profile = new Profile("test_name", "test_password", "test_recovery_code");

            Assert.IsFalse(profile.PasswordIsValid("password_123456"));
        }

        [TestCase]
        public void PasswordRecovery_CreatePasswordEntity_PasswordRecoveryIsEqual()
        {
            var profile = new Profile("test_name", "test_password", "test_recovery_code");

            Assert.AreEqual("test_password", profile.PasswordRecovery("test_recovery_code"));
        }


        [TestCase]
        public void PasswordRecovery_CreatedPasswordEntity_InvalidPasswordSecurityExceptionWasThrown()
        {
            var profile = new Profile("test_name", "test_password", "test_recovery_code");

            Assert.Catch<InvalidPasswordSecurityException>(() => {
                profile.PasswordRecovery("123456");
            });
        }
    }
}
