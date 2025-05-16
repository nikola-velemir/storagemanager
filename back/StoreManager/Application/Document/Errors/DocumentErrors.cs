using StoreManager.Application.Common;
using StoreManager.Application.Document.DTO;

namespace StoreManager.Application.Document.Errors;

public static class DocumentErrors
{
    public static Error DocumentNotFound => new Error("DocumentNotFound", 404,"Document not found");
    public static Error DocumentIdParseError => new Error("DocumentIdParseError", 400,"DocumentIdParse error");
    public static Result<DocumentDownloadResponseDto> DocumentChunkNotFound => new Error("DocumentChunkNotFound", 404,"Document Chunk Not Found");
}