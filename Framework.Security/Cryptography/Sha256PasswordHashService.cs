using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Framework.Security.Cryptography
{
    public class Sha256PasswordHashService : IPasswordHashService
    {
        private readonly SHA256 _sha256Algorithm = SHA256.Create();

        public string HashSaltAndPassword(string salt, string clearTextPassword)
        {
            return Hash(salt.Trim() + clearTextPassword.Trim());
        }

        private string Hash(string stringToHash)
        {
            byte[] rawHash = _sha256Algorithm.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
            return Encoding.UTF8.GetString(rawHash);
        }
    }
}
