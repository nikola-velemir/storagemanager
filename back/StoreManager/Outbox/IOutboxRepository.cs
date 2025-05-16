namespace StoreManager.outbox;

public interface IOutboxRepository
{
     Task InsertOutboxMessagesAsync<T>(
        T message,
        CancellationToken cancellationToken = default
    ) where T : notnull;
}