using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using StoreManager.Infrastructure.Auth.Command;
using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.Service;
namespace StoreManager.Infrastructure.Auth.Controller
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var response = await _mediator.Send(new LoginQuery(request.username, request.password));
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

                await _mediator.Send(new DeAuthenticateCommand(accessToken));

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
                var response = await _mediator.Send(new RefreshAuthentificationQuery(request.refresh_token));
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
