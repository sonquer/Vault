using Newtonsoft.Json;
using System;
using System.Text;
using Vault.Core.Dtos;
using Vault.Security;

namespace Vault.Core.Models
{
    /// <summary>
    /// Password entity
    /// </summary>
    public class Password
    {
        /// <summary>
        /// Unique password id
        /// </summary>
        public Guid Id { get; protected set; }
        
        /// <summary>
        /// Profile id
        /// </summary>
        public Guid ProfileId { get; protected set; }

        /// <summary>
        /// Encrypted Password DTO
        /// </summary>
        public string EncryptedJsonData { get; protected set; }

        /// <summary>
        /// Default password constructor
        /// </summary>
        /// <param name="profileId">Profile id</param>
        /// <param name="passwordDto">Password DTO</param>
        /// <param name="password">Plaintext password used to encrypt password DTO</param>
        public Password(Guid profileId, PasswordDto passwordDto, string password)
        {
            Id = Guid.NewGuid();
            ProfileId = profileId;
            UpdatePasswordDto(passwordDto, password);
        }

        protected Password()
        {
        }

        /// <summary>
        /// Decrypt password DTO using plaintext password
        /// </summary>
        /// <param name="password">Plaintext password</param>
        /// <returns></returns>
        public PasswordDto GetPasswordDto(string password)
        {
            var encryptedBytes = Convert.FromBase64String(EncryptedJsonData);
            var decryptedBytes = Cryptography.Decrypt(encryptedBytes, password);
            var decryptedJsonString = Encoding.UTF8.GetString(decryptedBytes);

            return JsonConvert.DeserializeObject<PasswordDto>(decryptedJsonString);
        }

        /// <summary>
        /// Update and encrypt password DTO
        /// </summary>
        /// <param name="passwordDto">Changed password DTO</param>
        /// <param name="password">Plaintext password</param>
        public void UpdatePasswordDto(PasswordDto passwordDto, string password)
        {
            var passwordDtoSerializedObject = JsonConvert.SerializeObject(passwordDto);
            var passwordDtoSerializedObjectAsBytes = Encoding.UTF8.GetBytes(passwordDtoSerializedObject);

            EncryptedJsonData = Convert.ToBase64String(Cryptography.Encrypt(passwordDtoSerializedObjectAsBytes, password));
        }
    }
}