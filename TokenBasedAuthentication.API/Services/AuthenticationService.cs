using System;
using TokenBasedAuthentication.API.Domain.Responses;
using TokenBasedAuthentication.API.Domain.Services;
using TokenBasedAuthentication.API.Security.Token;

namespace TokenBasedAuthentication.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public AccessTokenResponse CreateAccessToken(string emaill, string password)
        {
            UserResponse userResponse= _userService.FindByEmailAndPassword(emaill,password);

            if (userResponse.Success)
            {
                AccessToken accessToken= _tokenHandler.CreateAccessToken(userResponse.User);

                return new AccessTokenResponse(accessToken);
            }

            return new AccessTokenResponse(userResponse.Message);
        }

        public AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken)
        {
            UserResponse userResponse= _userService.GetUserWithRefreshToken(refreshToken);

            if (userResponse.Success)
            {
                if(userResponse.User.RefreshTokenEndDate<DateTime.Now)
                {
                    AccessToken accessToken= _tokenHandler.CreateAccessToken(userResponse.User);

                    return new AccessTokenResponse(accessToken);
                }

                return new AccessTokenResponse("The token has expired.");
            }

            return new AccessTokenResponse("Could not found user with refresh token");
        }

        public AccessTokenResponse RevokeRefreshToken(string refreshToken)
        {
            UserResponse userResponse= _userService.GetUserWithRefreshToken(refreshToken);

            if (userResponse.Success)
            {
                _userService.RevokeRefreshToken(userResponse.User);

                return new AccessTokenResponse(new AccessToken());
            }

            return new AccessTokenResponse("Could not found user with refresh token");
        }
    }
}