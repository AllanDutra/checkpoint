using System.Security.Cryptography;
using System.Text;

namespace Checkpoint.Core.DomainServices.Crypto
{
    public class CryptoDomainService : ICryptoDomainService
    {
        public string EncryptToSha256(string value)
        {
            string hash = string.Empty;

            foreach (byte theByte in SHA256.HashData(Encoding.ASCII.GetBytes(value)))
            {
                hash += theByte.ToString("x2");
            }

            return hash;
        }
    }
}
