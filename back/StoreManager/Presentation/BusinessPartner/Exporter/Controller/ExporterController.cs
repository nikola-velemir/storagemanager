using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.BusinessPartner.Exporter.Command;
using StoreManager.Application.BusinessPartner.Provider.Command.Statistics;
using StoreManager.Application.BusinessPartner.Provider.DTO;

namespace StoreManager.Presentation.BusinessPartner.Exporter.Controller;

[ApiController]
[Route("api/exporters")]
public class ExporterController(IMediator mediator) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody] ProviderCreateRequestDto request)
    {
        await mediator.Send(new CreateExporterCommand(request.Name, request.Address, request.PhoneNumber));
        return Ok();
    }

    [HttpGet("")]
    public async Task<IActionResult> FindAll()
    {
        return Ok(await mediator.Send(new FindAllExportersQuery()));
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> FindFiltered([FromQuery] string? exporterInfo, [FromQuery] int pageNumber,[FromQuery] int pageSize)
    {
        var result = await mediator.Send(new FindFilteredQuery(exporterInfo, PageNumber:pageNumber, PageSize:pageSize));
        return Ok(result);
    }
    [HttpGet("find-invoice-involvement")]
    public async Task<IActionResult> FindInvoiceInvolvement()
    {
        var result = await mediator.Send(new FindInvoiceInvolvementsQuery());
        return Ok(result);
    }
    [HttpGet("find-product-involvement")]
    public async Task<IActionResult> FindComponentInvolvement()
    {
        return Ok(await mediator.Send(new FindExporterProductInvolvementsQuery()));
    }
}