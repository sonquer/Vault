using NUnit.Framework;
using Vault.Core.Models;
using Vault.Security.Exceptions;

namespace Vault.Core.Tests
{
    public class ProfileTests
    {
        [TestCase]
        public void Create_profile_name_test()
        {
            var profile = new Profile("test_name", "test_password", "test_recovery_code");

            Assert.AreEqual("test_name", profile.Name);
        }

        [TestCase]
        public void Create_profile_password_is_valid_test()
        {
            var profile = new Profile("test_name", "test_password", "test_recovery_code");

            Assert.IsTrue(profile.PasswordIsValid("test_password"));
        }

        [TestCase]
        public void Create_profile_password_is_invalid_test()
        {
            var profile = new Profile("test_name", "test_password", "test_recovery_code");

            Assert.IsFalse(profile.PasswordIsValid("password_123456"));
        }

        [TestCase]
        public void Password_recovery_test()
        {
            var profile = new Profile("test_name", "test_password", "test_recovery_code");

            Assert.AreEqual("test_password", profile.PasswordRecovery("test_recovery_code"));
        }


        [TestCase]
        public void Invalid_recovery_code_test()
        {
            var profile = new Profile("test_name", "test_password", "test_recovery_code");

            Assert.Catch<InvalidPasswordSecurityException>(() => {
                profile.PasswordRecovery("123456");
            });
        }
    }
}
