using NUnit.Framework;
using Vault.Security.Random;

namespace Vault.Security.Tests.Random
{
    public class RecoveryCodeGeneratorTests
    {
        [TestCase]
        public void Random_hash_tests()
        {
            var random1 = RecoveryCodeGenerator.Instance.Generate();
            var random2 = RecoveryCodeGenerator.Instance.Generate();

            Assert.IsFalse(random1 == random2);
        }
    }
}
