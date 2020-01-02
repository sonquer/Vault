using System;
using System.Text;
using Vault.Security;
using Vault.Security.Exceptions;

namespace Vault.Core.Models
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string Name { get; protected set; }

        public string PasswordHash { get; protected set; }

        public string RecoveryCodeHash { get; protected set; }

        public string EncryptedRecoveryCode { get; protected set; }

        public Profile(string name, string password, string recoveryCode)
        {
            Name = name;
            PasswordHash = SecurePasswordHasher.Hash(password);
            RecoveryCodeHash = SecurePasswordHasher.Hash(recoveryCode);
            EncryptedRecoveryCode = Convert.ToBase64String(Cryptography.Encrypt(Encoding.UTF8.GetBytes(recoveryCode), recoveryCode));
        }

        protected Profile()
        {
        }

        public bool PasswordIsValid(string password) => SecurePasswordHasher.Verify(password, PasswordHash);

        public string PasswordRecovery(string recoveryCode)
        {
            if(!SecurePasswordHasher.Verify(recoveryCode, RecoveryCodeHash))
            {
                throw new InvalidPasswordSecurityException();
            }

            return Encoding.UTF8.GetString(Cryptography.Decrypt(Convert.FromBase64String(EncryptedRecoveryCode), recoveryCode));
        }
    }
}
