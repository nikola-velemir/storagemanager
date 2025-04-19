using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.Invoice.Base.Command;

namespace StoreManager.Presentation.Invoice.Base.Controller;

[ApiController]
[Route("api/invoices")]
public class InvoiceController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        var result = await mediator.Send(new FIndInvoiceTypeQuery(id));
        return Ok(result);
    }
}