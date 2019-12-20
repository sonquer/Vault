using Newtonsoft.Json;
using Vault.Core.Models;
using Vault.Infrastructure;
using Vault.Security;

namespace Vault.Console
{
    class Program
    {
        static void Main()
        {
            var profileRepository = new ProfileRepository();

            //var recoveryCode = Recovery.Generate();
            //profileRepository.Add(new Profile("test_name", "secret", recoveryCode));

            var profiles = profileRepository.GetAll();
            foreach (var profile in profiles)
            {
                System.Console.WriteLine(profile.PasswordIsValid("secret"));
                System.Console.WriteLine(profile.PasswordRecovery("1234"));
                System.Console.WriteLine(JsonConvert.SerializeObject(profile));
            }
        }
    }
}
