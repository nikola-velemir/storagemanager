using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.Service;
using StoreManager.Infrastructure.Auth.Tokens.RedisCache;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace StoreManager.Infrastructure.Auth.Controller
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRedisCacheService _redis;
        public AuthController(IAuthService loginService, IRedisCacheService redis)
        {
            this._authService = loginService;
            this._redis = redis;

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
            catch(BadHttpRequestException e)
            {
                return BadRequest(e.Message);
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
