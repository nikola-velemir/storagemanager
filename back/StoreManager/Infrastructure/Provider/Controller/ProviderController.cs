using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.Service;

namespace StoreManager.Infrastructure.Provider.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/providers")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _service;
        public ProviderController(IProviderService service)
        {
            _service = service;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] ProviderCreateRequestDTO request)
        {
            return Ok(await _service.Create(request));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] string id)
        {
            return Ok(await _service.FindById(Guid.Parse(id)));
        }
        [HttpGet("")]
        public async Task<IActionResult> FindAll()
        {
            return Ok(await _service.FindAll());
        }
        [HttpGet("filtered")]
        public async Task<IActionResult> FindFiltered([FromQuery] string? providerName, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await _service.FindFiltered(providerName, pageNumber, pageSize));
        }
        
    }
}
