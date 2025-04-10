namespace StoreManager.Infrastructure.Document.DTO
{
    public sealed record RequestDocumentDownloadResponseDto(string fileName, string type, int totalChunks);
}