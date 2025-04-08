using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Document.Command;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service;

namespace StoreManager.Infrastructure.Document.Controller
{
    [ApiController]
    [Route("api/docs")]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DocumentController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost("upload-chunks")]
        public async Task<ActionResult> UploadFileFromChunks([FromForm] string provider, [FromForm] IFormFile file, [FromForm] string fileName, [FromForm] int chunkIndex, [FromForm] int totalChunks)
        {
            try
            {
                await _mediator.Send(new UploadChunkCommand(provider, file, fileName, chunkIndex, totalChunks));
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
                var result = await _mediator.Send(new RequestDownloadQuery(invoiceId));
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
                var fileResponse = await _mediator.Send(new DownloadChunkQuery(invoiceId, chunkIndex));
                return new FileContentResult(fileResponse.bytes, fileResponse.mimeType);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
