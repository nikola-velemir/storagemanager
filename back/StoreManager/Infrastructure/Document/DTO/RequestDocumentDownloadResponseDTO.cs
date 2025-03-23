namespace StoreManager.Infrastructure.Document.DTO
{
    public sealed record class RequestDocumentDownloadResponseDTO(string fileName, string type, int totalChunks);
}
