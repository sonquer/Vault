using Newtonsoft.Json;
using Vault.Core.Dtos;
using Vault.Core.Models;
using Vault.Infrastructure;
using Vault.Core.Helpers;

namespace Vault.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var profileRepository = new ProfileRepository();

            //profileRepository.Add(new Profile(new ProfileDto { 
            //    Name = "Test123"
            //}, "secret"));

            var profiles = profileRepository.GetAll();
            foreach(var profile in profiles)
            {
                var profileDto = profile.GetProfileDto("secret");
                System.Console.WriteLine(JsonConvert.SerializeObject(profileDto));
            }
        }
    }
}
