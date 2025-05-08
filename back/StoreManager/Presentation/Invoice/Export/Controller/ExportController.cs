using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.Invoice.Export.Command;
using StoreManager.Application.Invoice.Export.DTO;
using StoreManager.Application.Invoice.Import.Command.Statistics;

namespace StoreManager.Presentation.Invoice.Export.Controller;

[ApiController]
[Route("api/exports")]
public class ExportController(IMediator mediator) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateExportRequestDto request)
    {
        var result = await mediator.Send(new CreateExportCommand(request.providerId, request.products));
        return FromResult(result);

    }

    [HttpGet("filtered")]
    public async Task<IActionResult> FindFiltered([FromQuery] string? date, [FromQuery] string? exporterId,
        [FromQuery] string? productInfo, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var result = await mediator.Send(new ExportSearchQuery(exporterId, productInfo, date, pageNumber, pageSize));
        return FromResult(result);

    }

    [HttpGet("find-counts-this-week")]
    public async Task<IActionResult> FindCountsThisWeek()
    {
        var result = await mediator.Send(new FindExportCountThisWeekQuery());

        return FromResult(result);

    }

    [HttpGet("count-this-week")]
    public async Task<IActionResult> CountExportsThisWeek()
    {
        var result = await mediator.Send(new CountExportsThisWeekQuery());

        return FromResult(result);

    }
}