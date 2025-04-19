using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.Invoice.Import.Command.Search;
using StoreManager.Application.Invoice.Import.Command.Statistics;

namespace StoreManager.Presentation.Invoice.Import.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/imports")]
    public class ImportController(IMediator mediator) : ControllerBase
    {
        [HttpGet("find-filtered")]
        public async Task<IActionResult> FindAll([FromQuery] string? componentInfo, [FromQuery] string? providerId, [FromQuery] string? date, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await mediator.Send(new FilterImportInvoicesQuery(componentInfo, providerId, date, pageNumber, pageSize));
            return Ok(result);
        }
        [HttpGet("find-counts-this-week")]
        public async Task<IActionResult> FindCountsThisWeek()
        {
            var result = await mediator.Send(new FindImportCountThisWeekQuery());
            return Ok(result);
        }
        [HttpGet("count-this-week")]
        public async Task<IActionResult> CountImportsThisWeek()
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
