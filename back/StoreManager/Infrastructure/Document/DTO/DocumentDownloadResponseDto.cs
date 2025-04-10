namespace StoreManager.Infrastructure.Document.DTO
{
    public sealed record DocumentDownloadResponseDto(byte[] bytes, string mimeType);
}