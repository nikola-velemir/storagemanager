namespace StoreManager.outbox;

public class DocumentProcessingRequest
{
    public Guid DocumentId { get; init; }
    public Guid ImportId { get; init; }
    public string MimeType { get; init; } = default!;
    public string FileName { get; init; } = default!;
}