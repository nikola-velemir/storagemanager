using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.Document.Command;

namespace StoreManager.Presentation.Document.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/docs")]
    public class DocumentController(IMediator mediator) : ApiControllerBase
    {
        [HttpPost("upload-chunks")]
        public async Task<IActionResult> UploadFileFromChunks([FromForm] string providerId, [FromForm] IFormFile file,
            [FromForm] string fileName, [FromForm] int chunkIndex, [FromForm] int totalChunks)
        {
            try
            {
                var result =
                    await mediator.Send(new UploadChunkCommand(providerId, file, fileName, chunkIndex, totalChunks));
                return FromResult(result);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("request-download/{invoiceId}")]
        public async Task<IActionResult> RequestDownload(string invoiceId)
        {
            var result = await mediator.Send(new RequestDownloadQuery(invoiceId));

            return FromResult(result);
        }

        [HttpGet("download-chunk")]
        public async Task<IActionResult> DownloadChunk([FromQuery] string invoiceId, [FromQuery] int chunkIndex)
        {
            var fileResponse = await mediator.Send(new DownloadChunkQuery(invoiceId, chunkIndex));
            if (fileResponse.IsSuccess)
                return File(fileResponse.Value.bytes, fileResponse.Value.mimeType);
            return BadRequest(new { fileResponse.Error.Name, fileResponse.Error.Description });
        }
    }
}