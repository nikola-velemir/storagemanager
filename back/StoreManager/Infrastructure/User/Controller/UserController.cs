using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.User.DTO;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Service;

namespace StoreManager.Infrastructure.User.Controller
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<UserCreateRequestDTO>> CreateUser([FromBody] UserCreateRequestDTO request)
        {
            return Ok(await _userService.CreateUser(request));
        }
    }
}
