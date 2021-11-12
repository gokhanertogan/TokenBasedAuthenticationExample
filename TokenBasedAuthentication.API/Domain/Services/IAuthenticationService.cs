using TokenBasedAuthentication.API.Domain.Responses;
using TokenBasedAuthentication.API.Security.Token;

namespace TokenBasedAuthentication.API.Domain.Services
{
    public interface IAuthenticationService
    {
        BaseResponse<AccessToken> CreateAccessToken(string emaill,string password);
        BaseResponse<AccessToken> CreateAccessTokenByRefreshToken(string refreshToken);
        BaseResponse<AccessToken> RevokeRefreshToken(string refreshToken);
    }
}