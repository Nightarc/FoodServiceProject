using System.Security.Cryptography;
using System.Text;

namespace VueAPI.Server.Helpers
{
    public static class PassHasher
    {
        public static string SHA256_hash(String value)
        {
            using (SHA256 hash = SHA256.Create())
            {
                return String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }
    }
}
