using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.BusinessPartner.Provider.Command;
using StoreManager.Application.BusinessPartner.Provider.Command.Info;
using StoreManager.Application.BusinessPartner.Provider.Command.Search;
using StoreManager.Application.BusinessPartner.Provider.Command.Statistics;
using StoreManager.Application.BusinessPartner.Provider.DTO;

namespace StoreManager.Presentation.BusinessPartner.Provider.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/providers")]
    public class ProviderController(IMediator mediator) : ApiControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] ProviderCreateRequestDto request)
        {
            var response =
                await mediator.Send(new CreateProviderCommand(request.Name, request.Address, request.PhoneNumber));

            return FromResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] string id)
        {
            var response = await mediator.Send(new FindProviderByIdQuery(id));

            return FromResult(response);

        }

        [HttpGet("")]
        public async Task<IActionResult> FindAll()
        {
            var response = await mediator.Send(new FindAllProvidersQuery());

            return FromResult(response);

        }

        [HttpGet("filtered")]
        public async Task<IActionResult> FindFiltered([FromQuery] string? providerName, [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var response = await mediator.Send(new FindFilteredProvidersQuery(providerName, pageNumber, pageSize));

            return FromResult(response);

        }

        [HttpGet("profile/{providerId}")]
        public async Task<IActionResult> FindProfile([FromRoute] string providerId)
        {
            var response = await mediator.Send(new FindProviderProfileQuery(providerId));
            return FromResult(response);

        }

        [HttpGet("find-invoice-involvement")]
        public async Task<IActionResult> FindInvoiceInvolvement()
        {
            var response = await mediator.Send(new FindProviderInvoiceInvolvementsQuery());
            return FromResult(response);

        }

        [HttpGet("find-component-involvement")]
        public async Task<IActionResult> FindComponentInvolvement()
        {
            var response = await mediator.Send(new FindProviderComponentInvolvementsQuery());
            return FromResult(response);

        }
    }
}