using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using StoreManager.Application.Auth.Command;
using StoreManager.Application.Auth.DTO;
using StoreManager.Application.Auth.Query;

namespace StoreManager.Presentation.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IMediator mediator) : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await mediator.Send(new LoginQuery(request.Username, request.Password));
            return FromResult(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            if (!Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
            {
                return BadRequest("Authorization missing");
            }


            var accessToken = authHeader.ToString().Substring("Bearer ".Length).Trim();

            var response = await mediator.Send(new DeAuthenticateCommand(accessToken));
            return FromResult(response);

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto request)
        {
                var response = await mediator.Send(new RefreshAuthenticationQuery(request.RefreshToken));
                return FromResult(response);

        }
    }
}