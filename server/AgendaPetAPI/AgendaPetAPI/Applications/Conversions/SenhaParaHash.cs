using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace AgendaPetAPI.Applications.Conversions
{
    public class SenhaHash
    {
        public static byte[] Converter(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senha);
                return sha256.ComputeHash(bytes);
            }
        }
    }
}
