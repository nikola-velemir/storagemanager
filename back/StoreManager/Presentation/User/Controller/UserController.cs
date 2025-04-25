using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.User.DTO;
using StoreManager.Domain.User.Service;

namespace StoreManager.Presentation.User.Controller
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
