using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.Service;

namespace StoreManager.Infrastructure.Auth.Controller
{
    [ApiController]
    [Route("api/auth")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            this._loginService = loginService;

        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var response = _loginService.Authenticate(request.username, request.password);
                return Ok(response);
            }
            catch(InvalidOperationException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch(UnauthorizedAccessException e) {
                return Unauthorized(new { message = e.Message });
            }
        }
    }
}
