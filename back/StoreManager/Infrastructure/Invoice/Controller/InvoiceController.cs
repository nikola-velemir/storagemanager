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

        [HttpGet("")]
        public async Task<IActionResult> FindAll()
        {
            return Ok(await _service.FindAll());
        }
    }
}
