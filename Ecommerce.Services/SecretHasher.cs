using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class SecretHasher
    {
        public static string GenerateSecretHash(string username, string clientId, string clientSecret)
        {
            var message = $"{username}{clientId}";
            var key = Encoding.UTF8.GetBytes(clientSecret);

            using (var hmac = new HMACSHA256(key))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return Convert.ToBase64String(hash);
            }
        }

    }
}
