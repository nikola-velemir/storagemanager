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
        [HttpPost("upload")]
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
        [HttpPost("upload-chunks")]
        public async Task<ActionResult> UploadFileFromChunks([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] int chunkIndex, [FromForm] int totalChunks)
        {
            try
            {
                await _service.UploadChunk(file,fileName,chunkIndex,totalChunks);
                return Ok(new { Message = "File uploaded successfully" });

            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpGet("download/{fileName}")]
        public async Task<ActionResult> DownloadFile(string fileName)
        {
            try
            {
                var response = await _service.DownloadFile(fileName);
               // return Ok(bytes);

                return File(response.bytes, response.mimeType, fileName);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Couldnt find the file");
            }
        }
    }
}
