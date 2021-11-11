using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TokenBasedAuthentication.API.Security.Token
{
    public static class SingHandler
    {
        public static SecurityKey GetSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}