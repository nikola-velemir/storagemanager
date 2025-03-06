using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var response = await _authService.Authenticate(request);
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

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            if (!Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
            {
                return BadRequest("Authorization missing");
            }
            try
            {

                var accessToken = authHeader.ToString().Substring("Bearer ".Length).Trim();

                await _authService.DeAuthenticate(accessToken);

                return Ok("Token revoked");

            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponseDTO>> Refresh([FromBody] RefreshRequestDTO request)
        {
            try
            {
                var response = await _authService.RefreshAuthentication(request);
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
