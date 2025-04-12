using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.MechanicalComponent.Command.Info;
using StoreManager.Infrastructure.MechanicalComponent.Command.Search;
using StoreManager.Infrastructure.MechanicalComponent.Command.Statistics;

namespace StoreManager.Infrastructure.MechanicalComponent.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/components")]
    public class MechanicalComponentController(IMediator service) : ControllerBase
    {
        [HttpGet("filtered")]
        public async Task<IActionResult> FindFiltered([FromQuery] string? providerId, [FromQuery] string? componentInfo, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var result = await service.Send(new FindFilteredComponentsQuery(providerId, componentInfo, pageNumber, pageSize));
            return Ok(result);
        }
        [HttpGet("filtered-product")]
        public async Task<IActionResult> FindFilteredProduct([FromQuery] string? providerId, [FromQuery] string? componentInfo, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var result = await service.Send(new FindFilteredComponentsForProductQuery(providerId, componentInfo, pageNumber, pageSize));
            return Ok(result);
        }
        [HttpGet("find-by-invoice/{invoiceId}")]
        public async Task<IActionResult> FindByInvoiceId([FromRoute] string invoiceId)
        {
            var result = await service.Send(new FindComponentByInvoiceIdQuery(invoiceId));
            return Ok(result);
        }
        [HttpGet("info/{componentId}")]
        public async Task<IActionResult> FindInfo([FromRoute] string componentId)
        {
            var result = await service.Send(new FindComponentInfoQuery(componentId));
            return Ok(result);
        }
        [HttpGet("find-quantity")]
        public async Task<IActionResult> FindQuantity()
        {
            var result = await service.Send(new FindComponentQuantitySumQuery());
            return Ok(result);
        }
        [HttpGet("find-top-five-quantity")]
        public async Task<IActionResult> FindTopFiveInQuantity()
        {
            var result = await service.Send(new FindTopFiveInQuantityQuery());
            return Ok(result);
        }
    }
}
