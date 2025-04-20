using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.Document.Command;

namespace StoreManager.Presentation.Document.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/docs")]
    public class DocumentController(IMediator mediator) : ControllerBase
    {
        [HttpPost("upload-chunks")]
        public async Task<ActionResult> UploadFileFromChunks([FromForm] string providerId, [FromForm] IFormFile file, [FromForm] string fileName, [FromForm] int chunkIndex, [FromForm] int totalChunks)
        {
            try
            {
                await mediator.Send(new UploadChunkCommand(providerId, file, fileName, chunkIndex, totalChunks));
                return Ok(new { Message = "File uploaded successfully" });

            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpGet("request-download/{invoiceId}")]
        public async Task<ActionResult> RequestDownload(string invoiceId)
        {
            try
            {
                var result = await mediator.Send(new RequestDownloadQuery(invoiceId));
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("download-chunk")]
        public async Task<ActionResult> DownloadChunk([FromQuery] string invoiceId, [FromQuery] int chunkIndex)
        {
            try
            {
                var fileResponse = await mediator.Send(new DownloadChunkQuery(invoiceId, chunkIndex));
                return new FileContentResult(fileResponse.bytes, fileResponse.mimeType);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
