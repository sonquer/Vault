using Newtonsoft.Json;
using System;
using System.Text;
using Vault.Core.Dtos;
using Vault.Security;

namespace Vault.Core.Models
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string EncryptedJsonData { get; set; }

        public Profile(ProfileDto profileDto, string password)
        {
            var profileDtoSerializedObject = JsonConvert.SerializeObject(profileDto);
            var profileDtoSerializedObjectAsBytes = Encoding.UTF8.GetBytes(profileDtoSerializedObject);

            var cryptograhy = new Cryptography();
            EncryptedJsonData = Convert.ToBase64String(cryptograhy.Encrypt(profileDtoSerializedObjectAsBytes, password));
        }

        protected Profile()
        {
        }
    }
}
