namespace StoreManager.Application.Document.DTO
{
    public sealed record DocumentDownloadResponseDto(byte[] bytes, string mimeType);
}