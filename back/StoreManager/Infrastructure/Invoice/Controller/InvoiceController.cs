using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Invoice.Command.Search;
using StoreManager.Infrastructure.Invoice.Command.Statistics;

namespace StoreManager.Infrastructure.Invoice.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/invoices")]
    public class InvoiceController(IMediator mediator) : ControllerBase
    {
        [HttpGet("find-filtered")]
        public async Task<IActionResult> FindAll([FromQuery] string? componentInfo, [FromQuery] string? providerId, [FromQuery] string? date, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await mediator.Send(new FilterInvoicesQuery(componentInfo, providerId, date, pageNumber, pageSize));
            return Ok(result);
        }
        [HttpGet("find-counts-this-week")]
        public async Task<IActionResult> FindCountsThisWeek()
        {
            var result = await mediator.Send(new FindCountsThisWeekQuery());
            return Ok(result);
        }
        [HttpGet("count-this-week")]
        public async Task<IActionResult> CountInvoicesThisWeek()
        {
            var result = await mediator.Send(new CountInvoicesThisWeekQuery());
            return Ok(result);
        }
        [HttpGet("total-value")]
        public async Task<IActionResult> FindTotalInventoryValue()
        {
            var result = await mediator.Send(new FindTotalInventoryValueQuery());
            return Ok(result);
        }
    }
}
