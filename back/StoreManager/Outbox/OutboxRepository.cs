using System.Text.Json;
using StoreManager.Infrastructure.Context;

namespace StoreManager.outbox;

public class OutboxRepository(WarehouseDbContext context) : IOutboxRepository
{
    public async Task InsertOutboxMessagesAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : notnull
    {
        var outboxMessage = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Payload = JsonSerializer.Serialize(message),
            Type = message.GetType().Name!,
            CreatedAt = DateTime.UtcNow
        };
        await context.OutboxMessages.AddAsync(outboxMessage, cancellationToken);

    }
}