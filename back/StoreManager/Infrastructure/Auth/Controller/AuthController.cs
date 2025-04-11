using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using StoreManager.Infrastructure.Auth.Command;
using StoreManager.Infrastructure.Auth.DTO;

namespace StoreManager.Infrastructure.Auth.Controller
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var response = await mediator.Send(new LoginQuery(request.username, request.password));
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

                await mediator.Send(new DeAuthenticateCommand(accessToken));

                return Ok("Token revoked");

            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponseDto>> Refresh([FromBody] RefreshRequestDto request)
        {
            try
            {
                var response = await mediator.Send(new RefreshAuthenticationQuery(request.RefreshToken));
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
