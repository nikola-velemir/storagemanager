namespace StoreManager.outbox;

public class OutboxMessage
{
    public Guid Id { get; init; }
    public string Type { get; init; } = default!;
    public string Payload { get; init; } = default!;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; }
    public string? Error { get; set; }
}