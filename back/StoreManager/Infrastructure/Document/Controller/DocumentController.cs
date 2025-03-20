using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service;

namespace StoreManager.Infrastructure.Document.Controller
{
    [ApiController]
    [Route("api/upload")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;
        public DocumentController(IDocumentService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            try
            {
                await _service.UploadFile(file);
                return Ok(new { Message = "File uploaded successfully" });

            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
