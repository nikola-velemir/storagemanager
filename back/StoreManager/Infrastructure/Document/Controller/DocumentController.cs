using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service;

namespace StoreManager.Infrastructure.Document.Controller
{
    [ApiController]
    [Route("api/docs")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;
        public DocumentController(IDocumentService service)
        {
            _service = service;
        }
        
        [HttpPost("upload-chunks")]
        public async Task<ActionResult> UploadFileFromChunks([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] int chunkIndex, [FromForm] int totalChunks)
        {
            try
            {
                await _service.UploadChunk(file, fileName, chunkIndex, totalChunks);
                return Ok(new { Message = "File uploaded successfully" });

            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpGet("request-download/{fileName}")]
        public async Task<ActionResult> RequestDownload(string fileName)
        {
            try
            {

                return Ok(await _service.RequestDownload(fileName));
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("download-chunk")]
        public async Task<ActionResult> DownloadChunk([FromQuery] string fileName, [FromQuery] int chunkIndex)
        {
            try
            {
                var fileResponse = await _service.DownloadChunk(fileName, chunkIndex);
                return new FileContentResult(fileResponse.bytes, fileResponse.mimeType);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
