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

        public string EncryptedJsonData { get; protected set; }

        public Password(PasswordDto passwordDto, string password)
        {
            var passwordDtoSerializedObject = JsonConvert.SerializeObject(passwordDto);
            var passwordDtoSerializedObjectAsBytes = Encoding.UTF8.GetBytes(passwordDtoSerializedObject);

            var cryptograhy = new Cryptography();
            EncryptedJsonData = Convert.ToBase64String(cryptograhy.Encrypt(passwordDtoSerializedObjectAsBytes, password));
        }

        protected Password()
        {
        }
    }
}