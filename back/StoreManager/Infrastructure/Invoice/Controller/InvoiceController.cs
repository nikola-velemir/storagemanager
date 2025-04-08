using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Invoice.Query;
using StoreManager.Infrastructure.Invoice.Service;

namespace StoreManager.Infrastructure.Invoice.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("find-filtered")]
        public async Task<IActionResult> FindAll([FromQuery] string? componentInfo, [FromQuery] string? providerId, [FromQuery] string? date, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _mediator.Send(new FilterInvoicesQuery(componentInfo, providerId, date, pageNumber, pageSize));
            return Ok(result);
        }
    }
}
