using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.Invoice.Export.Command;
using StoreManager.Application.Invoice.Export.DTO;

namespace StoreManager.Presentation.Invoice.Export.Controller;

[ApiController]
[Route("api/exports")]
public class ExportController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateExportRequestDto request)
    {
        await mediator.Send(new CreateExportCommand(request.providerId, request.products));
        return Ok();
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> FindFiltered([FromQuery] string? date, [FromQuery] string? exporterId,
        [FromQuery] string? productInfo, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var result = await mediator.Send(new ExportSearchQuery(exporterId, productInfo, date, pageNumber, pageSize));
        return Ok(result);
    }
}