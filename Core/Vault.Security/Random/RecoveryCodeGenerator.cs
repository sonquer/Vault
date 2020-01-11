using System.Security.Cryptography;
using System.Text;

namespace Vault.Security.Random
{
    public class RecoveryCodeGenerator
    {
        protected RecoveryCodeGenerator()
        {
        }

        private static RecoveryCodeGenerator _instance;

        public static RecoveryCodeGenerator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RecoveryCodeGenerator();
                }

                return _instance;
            }
        }

        public string Generate(int length = 10)
        {
            var seed = "QWERTYUIOPASDFGHJKLZXCVBNMqazwsxedcrfvtgbyhnujmiklop1234567890!@#$%^&*()_+";
            var random = new System.Random();
            var result = new StringBuilder();

            for(int i = 0; i<length; i++)
            {
                var randomNumber = random.Next(seed.Length - 1);
                var randomCharacter = seed[randomNumber];

                result.Append(randomCharacter);
            }

            using (var sha256 = SHA256.Create())
            {
                var computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(result.ToString()));

                result.Clear();

                foreach (byte @byte in computedHash)
                {
                    result.Append(@byte.ToString("x2"));
                }

                return result.ToString();
            }
        }
    }
}
