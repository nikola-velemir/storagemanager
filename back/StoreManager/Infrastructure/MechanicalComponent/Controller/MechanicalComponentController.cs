using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.MechanicalComponent.Command;
using StoreManager.Infrastructure.MechanicalComponent.Service;

namespace StoreManager.Infrastructure.MechanicalComponent.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/components")]
    public class MechanicalComponentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MechanicalComponentController(IMediator service)
        {
            _mediator = service;
        }
        [HttpGet("filtered")]
        public async Task<IActionResult> FindFiltered([FromQuery] string? providerId, [FromQuery] string? componentInfo, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var result = await _mediator.Send(new FindFilteredComponentsQuery(providerId, componentInfo, pageNumber, pageSize));
            return Ok(result);
        }
        [HttpGet("find-by-invoice/{invoiceId}")]
        public async Task<IActionResult> FindByInvoiceId([FromRoute] string invoiceId)
        {
            var result = await _mediator.Send(new FindComponentByInvoiceIdQuery(invoiceId));
            return Ok(result);
        }
        [HttpGet("info/{componentId}")]
        public async Task<IActionResult> FindInfo([FromRoute] string componentId)
        {
            var result = await _mediator.Send(new FindComponentInfoQuery(componentId));
            return Ok(result);
        }
    }
}
