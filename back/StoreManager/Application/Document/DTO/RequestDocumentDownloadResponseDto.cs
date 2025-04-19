namespace StoreManager.Application.Document.DTO
{
    public sealed record RequestDocumentDownloadResponseDto(string fileName, string type, int totalChunks);
}