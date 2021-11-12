using Microsoft.AspNetCore.Mvc;
using TokenBasedAuthentication.API.Domain.Services;
using TokenBasedAuthentication.API.Extensions;
using TokenBasedAuthentication.API.Resources;

namespace TokenBasedAuthentication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult AccessToken(LoginResource loginResource)
        {
            if (ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var accessResponse = _authenticationService.CreateAccessToken(loginResource.Email, loginResource.Password);

            if (accessResponse.Success)
                return Ok(accessResponse.Model);

            return BadRequest(accessResponse.Message);
        }

        [HttpPost]
        public IActionResult RefreshToken(TokenResource tokenResource)
        {
            var accessResponse = _authenticationService.CreateAccessTokenByRefreshToken(tokenResource.RefreshToken);

            if (accessResponse.Success)
                return Ok(accessResponse.Model);

            return BadRequest(accessResponse.Message);
        }

        [HttpPost]
        public IActionResult RevokeRefreshToken(TokenResource tokenResource)
        {
            var accessResponse = _authenticationService.RevokeRefreshToken(tokenResource.RefreshToken);

            if (accessResponse.Success)
                return Ok(accessResponse.Model);

            return BadRequest(accessResponse.Message);
        }
    }
}