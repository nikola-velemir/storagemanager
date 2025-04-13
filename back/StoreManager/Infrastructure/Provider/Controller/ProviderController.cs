using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Provider.Command;
using StoreManager.Infrastructure.Provider.Command.Info;
using StoreManager.Infrastructure.Provider.Command.Search;
using StoreManager.Infrastructure.Provider.Command.Statistics;
using StoreManager.Infrastructure.Provider.DTO;

namespace StoreManager.Infrastructure.Provider.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/providers")]
    public class ProviderController(IMediator service) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] ProviderCreateRequestDto request)
        {
            return Ok(await service.Send(new CreateProviderCommand(request.Name, request.Address, request.PhoneNumber)));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] string id)
        {
            return Ok(await service.Send(new FindProviderByIdQuery(id)));
        }
        [HttpGet("")]
        public async Task<IActionResult> FindAll()
        {
            return Ok(await service.Send(new FindAllProvidersQuery()));
        }
        [HttpGet("filtered")]
        public async Task<IActionResult> FindFiltered([FromQuery] string? providerName, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await service.Send(new FindFilteredProvidersQuery(providerName, pageNumber, pageSize)));
        }
        [HttpGet("profile/{providerId}")]
        public async Task<IActionResult> FindProfile([FromRoute] string providerId)
        {
            return Ok(await service.Send(new FindProviderProfileQuery(providerId)));
        }
        [HttpGet("find-invoice-involvement")]
        public async Task<IActionResult> FindInvoiceInvolvement()
        {
            return Ok(await service.Send(new FindProviderInvoiceInvolvementsQuery()));
        }
        [HttpGet("find-component-involvement")]
        public async Task<IActionResult> FindComponentInvolvement()
        {
            return Ok(await service.Send(new FindProviderComponentInvolvementsQuery()));
        }
    }
}
