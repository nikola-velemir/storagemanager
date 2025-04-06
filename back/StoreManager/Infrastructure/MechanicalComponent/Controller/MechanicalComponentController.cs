using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.MechanicalComponent.Service;

namespace StoreManager.Infrastructure.MechanicalComponent.Controller
{
    [ApiController]
    [Route("api/components")]
    public class MechanicalComponentController : ControllerBase
    {
        private readonly IMechanicalComponentService _service;

        public MechanicalComponentController(IMechanicalComponentService service)
        {
            _service = service;
        }
        [HttpGet("filtered")]
        public async Task<IActionResult> FindFiltered([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            return Ok(await _service.FindFiltered(pageNumber, pageSize));
        }
    }
}
