using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.Service;

namespace StoreManager.Infrastructure.Auth.Controller
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService loginService)
        {
            this._authService = loginService;

        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var response = _authService.Authenticate(request);
                return Ok(response);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch (UnauthorizedAccessException e)
            {
                return NotFound(new { message = e.Message });
            }
        }
        [HttpPost("refresh")]
        public ActionResult Refresh([FromBody] RefreshRequestDTO request)
        {
            try
            {
                var response = _authService.RefreshAuthentication(request);
                return Ok(response);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch (TimeoutException)
            {
                return StatusCode(408, "Refresh timed out");
            }
        }
    }
}
