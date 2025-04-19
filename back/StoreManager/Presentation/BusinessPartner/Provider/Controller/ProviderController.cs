using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.BusinessPartner.Provider.Command.Info;
using StoreManager.Application.BusinessPartner.Provider.Command.Search;
using StoreManager.Application.BusinessPartner.Provider.Command.Statistics;
using StoreManager.Application.BusinessPartner.Provider.DTO;
using StoreManager.Domain.BusinessPartner.Provider.Command;

namespace StoreManager.Presentation.BusinessPartner.Provider.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/providers")]
    public class ProviderController(IMediator mediator) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] ProviderCreateRequestDto request)
        {
            return Ok(await mediator.Send(new CreateProviderCommand(request.Name, request.Address, request.PhoneNumber)));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] string id)
        {
            return Ok(await mediator.Send(new FindProviderByIdQuery(id)));
        }
        [HttpGet("")]
        public async Task<IActionResult> FindAll()
        {
            return Ok(await mediator.Send(new FindAllProvidersQuery()));
        }
        [HttpGet("filtered")]
        public async Task<IActionResult> FindFiltered([FromQuery] string? providerName, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await mediator.Send(new FindFilteredProvidersQuery(providerName, pageNumber, pageSize)));
        }
        [HttpGet("profile/{providerId}")]
        public async Task<IActionResult> FindProfile([FromRoute] string providerId)
        {
            return Ok(await mediator.Send(new FindProviderProfileQuery(providerId)));
        }
        [HttpGet("find-invoice-involvement")]
        public async Task<IActionResult> FindInvoiceInvolvement()
        {
            return Ok(await mediator.Send(new FindProviderInvoiceInvolvementsQuery()));
        }
        [HttpGet("find-component-involvement")]
        public async Task<IActionResult> FindComponentInvolvement()
        {
            return Ok(await mediator.Send(new FindProviderComponentInvolvementsQuery()));
        }
    }
}
