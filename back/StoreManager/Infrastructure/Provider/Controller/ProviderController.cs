using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Provider.Command;
using StoreManager.Infrastructure.Provider.DTO;

namespace StoreManager.Infrastructure.Provider.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/providers")]
    public class ProviderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProviderController(IMediator service)
        {
            _mediator = service;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] ProviderCreateRequestDTO request)
        {
            return Ok(await _mediator.Send(new CreateProviderCommand(request.name, request.address, request.phoneNumber)));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] string id)
        {
            return Ok(await _mediator.Send(new FindProviderByIdQuery(id)));
        }
        [HttpGet("")]
        public async Task<IActionResult> FindAll()
        {
            return Ok(await _mediator.Send(new FindAllProvidersQuery()));
        }
        [HttpGet("filtered")]
        public async Task<IActionResult> FindFiltered([FromQuery] string? providerName, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await _mediator.Send(new FindFilteredProvidersQuery(providerName, pageNumber, pageSize)));
        }
        [HttpGet("profile/{providerId}")]
        public async Task<IActionResult> FindProfile([FromRoute] string providerId)
        {
            return Ok(await _mediator.Send(new FindProviderProfileQuery(providerId)));
        }

    }
}
