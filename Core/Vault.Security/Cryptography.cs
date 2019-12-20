using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Vault.Security
{
    public sealed class Cryptography
    {
        /// <summary>
        /// Encrypt data using AES algorithm
        /// </summary>
        /// <param name="bytesToEncrypt">Bytes to be encrypted</param>
        /// <param name="passsord">Raw password</param>
        /// <returns>Encrypted bytes</returns>
        public static byte[] Encrypt(byte[] bytesToEncrypt, string passsord)
        {
            var hash = SHA256.Create();

            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(hash.ComputeHash(Encoding.UTF8.GetBytes(passsord)), saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        /// <summary>
        /// Decrypt data using AES algorithm.
        /// </summary>
        /// <param name="bytesToDecrypt">Bytes to be decrypted</param>
        /// <param name="password">Raw password</param>
        /// <returns>Decrypted bytes</returns>
        public static byte[] Decrypt(byte[] bytesToDecrypt, string password)
        {
            var hash = SHA256.Create();

            byte[] decryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(hash.ComputeHash(Encoding.UTF8.GetBytes(password)), saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToDecrypt, 0, bytesToDecrypt.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }
    }
}
