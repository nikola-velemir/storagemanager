using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.MechanicalComponent.Service;

namespace StoreManager.Infrastructure.MechanicalComponent.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/components")]
    public class MechanicalComponentController : ControllerBase
    {
        private readonly IMechanicalComponentService _service;

        public MechanicalComponentController(IMechanicalComponentService service)
        {
            _service = service;
        }
        [HttpGet("filtered")]
        public async Task<IActionResult> FindFiltered([FromQuery] string? providerId, [FromQuery] string? componentInfo, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            return Ok(await _service.FindFiltered(providerId, componentInfo, pageNumber, pageSize));
        }
        [HttpGet("find-by-invoice/{invoiceId}")]
        public async Task<IActionResult> FindByInvoiceId([FromRoute] string invoiceId)
        {
            return Ok(await _service.FindByInvoiceId(invoiceId));
        }
        [HttpGet("info/{componentId}")]
        public async Task<IActionResult> FindInfo([FromRoute] string componentId)
        {
            return Ok(await _service.FindInfo(componentId));
        }
    }
}
