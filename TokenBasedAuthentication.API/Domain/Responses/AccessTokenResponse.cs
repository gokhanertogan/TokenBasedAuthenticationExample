using TokenBasedAuthentication.API.Security.Token;

namespace TokenBasedAuthentication.API.Domain.Responses
{
    public class AccessTokenResponse : BaseResponse
    {
        public AccessToken AccessToken { get; set; }

        private AccessTokenResponse(bool success, string message, AccessToken accesstToken) : base(success, message)
        {
            AccessToken = accesstToken;
        }

        public AccessTokenResponse(AccessToken accessToken) : this(true, string.Empty, accessToken)
        {

        }

        public AccessTokenResponse(string message) : this(false, message,null)
        {

        }

    }
}