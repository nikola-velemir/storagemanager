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
        public ActionResult<Boolean> CreateUser([FromBody] UserCreateRequestDTO request) {  
            _userService.CreateUser(request);
            return true;
        }
    }
}
