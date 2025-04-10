using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.User.DTO;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Service;

namespace StoreManager.Infrastructure.User.Controller
{
    [ApiController]
    [Route("api/users")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<UserCreateRequestDto>> CreateUser([FromBody] UserCreateRequestDto request)
        {
            return Ok(await userService.CreateUser(request));
        }
    }
}
