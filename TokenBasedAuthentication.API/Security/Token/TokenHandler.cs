using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TokenBasedAuthentication.API.Domain.Model;

namespace TokenBasedAuthentication.API.Security.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenOptions _tokenOptions;
        public TokenHandler(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SingHandler.GetSecurityKey(_tokenOptions.SecurityKey);

            SigningCredentials mySigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: _tokenOptions.Issuer,
                    audience: _tokenOptions.Audience,
                    expires: accessTokenExpiration,
                    notBefore: DateTime.Now.AddMinutes(5),
                    claims:GetClaim(user),
                    signingCredentials: mySigningCredentials
            );

            var handler= new JwtSecurityTokenHandler();
            var token= handler.WriteToken(jwtSecurityToken);

            AccessToken accessToken=new AccessToken();
            accessToken.Token= token;
            accessToken.RefreshToken=CreateRefreshToken();
            accessToken.Expiration=accessTokenExpiration;

            return accessToken;
        }

        private IEnumerable<Claim> GetClaim(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.SurName}"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            return claims;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using(var rng= RandomNumberGenerator.Create())
            {
                rng.GetBytes(numberByte);
                return Convert.ToBase64String(numberByte);
            }
        }

        public void RevokeRefreshToken(User user)
        {
            user.RefreshToken=null;            
        }
    }
}