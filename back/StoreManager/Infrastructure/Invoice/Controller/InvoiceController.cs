using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Invoice.Service;

namespace StoreManager.Infrastructure.Invoice.Controller
{
    [ApiController]
    [Route("api/invoice")]
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
