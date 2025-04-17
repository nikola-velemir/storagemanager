using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Command;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Controller;

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
}