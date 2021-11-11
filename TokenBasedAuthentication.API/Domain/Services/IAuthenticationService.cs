using TokenBasedAuthentication.API.Domain.Responses;

namespace TokenBasedAuthentication.API.Domain.Services
{
    public interface IAuthenticationService
    {
        AccessTokenResponse CreateAccessToken(string emaill,string password);
        AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken);
        AccessTokenResponse RevokeRefreshToken(string refreshToken);
    }
}