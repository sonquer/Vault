using System.Security.Cryptography;
using System.Text;

namespace Vault.Security.Random
{
    /// <summary>
    /// Recovery code generator
    /// </summary>
    public class RecoveryCodeGenerator
    {
        protected RecoveryCodeGenerator()
        {
        }

        private static RecoveryCodeGenerator _instance;

        /// <summary>
        /// Get instance
        /// </summary>
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

        /// <summary>
        /// Generate password recovery SHA256
        /// </summary>
        /// <param name="length">Length of random string used to generate SHA256</param>
        /// <returns></returns>
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
