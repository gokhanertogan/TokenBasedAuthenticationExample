using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenBasedAuthentication.API.Domain.Model;
using TokenBasedAuthentication.API.Domain.Responses;
using TokenBasedAuthentication.API.Domain.Services;
using TokenBasedAuthentication.API.Resources;

namespace TokenBasedAuthentication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        public  IActionResult GetUser()
        {
            IEnumerable<Claim> claims = User.Claims;
            string userId = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value;
            BaseResponse<User> userResponse =  _userService.FindById(int.Parse(userId));

            if (userResponse.Success)
            {
                return Ok(userResponse);
            }
            else
            {
                return BadRequest(userResponse.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser(UserResource userResource)
        {
            User user = _mapper.Map<UserResource, User>(userResource);
            BaseResponse<User> userResponse = _userService.AddUser(user);

            if (userResponse.Success)
            {
                return Ok(userResponse.Model);
            }
            else
            {
                return BadRequest(userResponse.Message);
            }
        }
    }
}