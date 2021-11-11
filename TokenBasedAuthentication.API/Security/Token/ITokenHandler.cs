using TokenBasedAuthentication.API.Domain.Model;

namespace TokenBasedAuthentication.API.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);
        void RevokeRefreshToken(User user);
    }
}