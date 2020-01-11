using Newtonsoft.Json;
using System;
using System.Text;
using Vault.Core.Dtos;
using Vault.Security;

namespace Vault.Core.Models
{
    public class Password
    {
        public Guid Id { get; protected set; }
        
        public Guid ProfileId { get; protected set; }

        public string EncryptedJsonData { get; protected set; }

        public Password(Guid profileId, PasswordDto passwordDto, string password)
        {
            ProfileId = profileId;
            UpdatePasswordDto(passwordDto, password);
        }

        protected Password()
        {
        }

        public PasswordDto GetPasswordDto(string password)
        {
            var encryptedBytes = Convert.FromBase64String(EncryptedJsonData);
            var decryptedBytes = Cryptography.Decrypt(encryptedBytes, password);
            var decryptedJsonString = Encoding.UTF8.GetString(decryptedBytes);

            return JsonConvert.DeserializeObject<PasswordDto>(decryptedJsonString);
        }

        public void UpdatePasswordDto(PasswordDto passwordDto, string password)
        {
            var passwordDtoSerializedObject = JsonConvert.SerializeObject(passwordDto);
            var passwordDtoSerializedObjectAsBytes = Encoding.UTF8.GetBytes(passwordDtoSerializedObject);

            EncryptedJsonData = Convert.ToBase64String(Cryptography.Encrypt(passwordDtoSerializedObjectAsBytes, password));
        }
    }
}