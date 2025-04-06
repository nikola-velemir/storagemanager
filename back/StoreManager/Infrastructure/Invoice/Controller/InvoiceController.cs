using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Invoice.Service;

namespace StoreManager.Infrastructure.Invoice.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _service;
        public InvoiceController(IInvoiceService service)
        {
            _service = service;
        }

        [HttpGet("find-filtered")]
        public async Task<IActionResult> FindAll([FromQuery] string? componentInfo,[FromQuery] string? providerId, [FromQuery] string? date, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await _service.FindFilteredInvoices(componentInfo, providerId, date, pageNumber, pageSize));
        }
    }
}
