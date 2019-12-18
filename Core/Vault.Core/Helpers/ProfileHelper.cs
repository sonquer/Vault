using Newtonsoft.Json;
using System;
using System.Text;
using Vault.Core.Dtos;
using Vault.Core.Models;
using Vault.Security;

namespace Vault.Core.Helpers
{
    public static class ProfileHelper
    {
        public static ProfileDto GetProfileDto(this Profile profileModel, string password)
        {
            var encryptedProfileDtoAsBytes = Convert.FromBase64String(profileModel.EncryptedJsonData);
            var cryptography = new Cryptography();
            var profileAsBytes = cryptography.Decrypt(encryptedProfileDtoAsBytes, password);
            var profileAsJsonString = Encoding.UTF8.GetString(profileAsBytes);

            return JsonConvert.DeserializeObject<ProfileDto>(profileAsJsonString);
        }
    }
}
