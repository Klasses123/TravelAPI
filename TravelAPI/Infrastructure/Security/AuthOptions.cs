using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TravelAPI.Infrastructure.Security
{
    public class AuthOptions
    {
        public const string ISSUER = "Klasses"; // издатель токена
        public const string AUDIENCE = "Client"; // потребитель токена
        const string KEY = "010203Qwe@";   // ключ для шифрации
        public const int LIFETIME = 60; // время жизни токена - 60 минута
        public static SecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
