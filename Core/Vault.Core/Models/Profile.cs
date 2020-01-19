using System;
using System.Text;
using Vault.Security;
using Vault.Security.Exceptions;

namespace Vault.Core.Models
{
    /// <summary>
    /// Profile entity
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Profile unique id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Profile name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Profile password hash
        /// </summary>
        public string PasswordHash { get; protected set; }

        /// <summary>
        /// Profile recovery code hash
        /// </summary>
        public string RecoveryCodeHash { get; protected set; }

        /// <summary>
        /// Encrypted user password by recovery code
        /// </summary>
        public string EncryptedRecoveryCode { get; protected set; }

        /// <summary>
        /// Default profile constructor
        /// </summary>
        /// <param name="name">Profile name</param>
        /// <param name="password">Profile password</param>
        /// <param name="recoveryCode">Generated recovery code</param>
        public Profile(string name, string password, string recoveryCode)
        {
            Id = Guid.NewGuid();
            Name = name;
            PasswordHash = SecurePasswordHasher.Hash(password);
            RecoveryCodeHash = SecurePasswordHasher.Hash(recoveryCode);
            EncryptedRecoveryCode = Convert.ToBase64String(Cryptography.Encrypt(Encoding.UTF8.GetBytes(password), recoveryCode));
        }

        protected Profile()
        {
        }

        /// <summary>
        /// Check password is valid
        /// </summary>
        /// <param name="password">Plaintext password</param>
        /// <returns></returns>
        public bool PasswordIsValid(string password) => SecurePasswordHasher.Verify(password, PasswordHash);

        /// <summary>
        /// Plaintext password by recovery code
        /// </summary>
        /// <param name="recoveryCode"></param>
        /// <returns></returns>
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
