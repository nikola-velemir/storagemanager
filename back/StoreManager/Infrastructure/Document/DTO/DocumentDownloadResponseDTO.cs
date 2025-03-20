namespace StoreManager.Infrastructure.Document.DTO
{
    public sealed record class DocumentDownloadResponseDTO(byte[] bytes,string mimeType);
}
