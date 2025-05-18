using MediatR;

namespace StoreManager.Infrastructure.Notifications;

public record DocumentProcessedNotification(Guid DocumentId, string FileName) : INotification;