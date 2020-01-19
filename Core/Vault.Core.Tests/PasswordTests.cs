using NUnit.Framework;
using System;
using Vault.Core.Dtos;
using Vault.Core.Models;

namespace Vault.Core.Tests
{
    public class PasswordTests
    {
        [TestCase]
        public void Create_password_id_is_unique_test()
        {
            var passwordDto = new PasswordDto
            {
                Email = "test_email",
                Description = "test_description",
                Name = "test_name",
                Password = "test_password"
            };

            var password = new Password(Guid.NewGuid(), passwordDto, "test_password");

            Assert.IsTrue(password.Id != Guid.Empty);
        }

        [TestCase]
        public void Create_password_dto_test()
        {
            var passwordDto = new PasswordDto
            {
                Email = "test_email",
                Description = "test_description",
                Name = "test_name",
                Password = "test_password"
            };

            var password = new Password(Guid.NewGuid(), passwordDto, "test_password");

            Assert.IsTrue(password.EncryptedJsonData.Length > 0);
        }

        [TestCase]
        public void Get_password_dto_test()
        {
            var passwordDto = new PasswordDto
            {
                Email = "test_email",
                Description = "test_description",
                Name = "test_name",
                Password = "test_password"
            };

            var password = new Password(Guid.NewGuid(), passwordDto, "test_password");

            var dto = password.GetPasswordDto("test_password");

            Assert.AreEqual(passwordDto.Email, dto.Email);
            Assert.AreEqual(passwordDto.Description, dto.Description);
            Assert.AreEqual(passwordDto.Name, dto.Name);
            Assert.AreEqual(passwordDto.Password, dto.Password);
        }
    }
}
