using System;
using TokenBasedAuthentication.API.Domain.Model;
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

        public BaseResponse<AccessToken> CreateAccessToken(string emaill, string password)
        {
            BaseResponse<User> userResponse = _userService.FindByEmailAndPassword(emaill, password);

            if (userResponse.Success)
            {
                AccessToken accessToken = _tokenHandler.CreateAccessToken(userResponse.Model);
                _userService.SaveRefreshToken(userResponse.Model.Id, accessToken.RefreshToken);
                return new BaseResponse<AccessToken>(accessToken);
            }

            return new BaseResponse<AccessToken>(userResponse.Message);
        }

        public BaseResponse<AccessToken> CreateAccessTokenByRefreshToken(string refreshToken)
        {
            BaseResponse<User> userResponse = _userService.GetUserWithRefreshToken(refreshToken);

            if (userResponse.Success)
            {
                if (userResponse.Model.RefreshTokenEndDate > DateTime.Now)
                {
                    AccessToken accessToken = _tokenHandler.CreateAccessToken(userResponse.Model);
                    _userService.SaveRefreshToken(userResponse.Model.Id, accessToken.RefreshToken);
                    return new BaseResponse<AccessToken>(accessToken);
                }

                return new BaseResponse<AccessToken>("The token has expired.");
            }

            return new BaseResponse<AccessToken>("Could not found user with refresh token");
        }

        public BaseResponse<AccessToken> RevokeRefreshToken(string refreshToken)
        {
            BaseResponse<User> userResponse = _userService.GetUserWithRefreshToken(refreshToken);

            if (userResponse.Success)
            {
                _userService.RevokeRefreshToken(userResponse.Model);

                return new BaseResponse<AccessToken>(new AccessToken());
            }

            return new BaseResponse<AccessToken>("Could not found user with refresh token");
        }
    }
}