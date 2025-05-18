using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace StoreManager.Infrastructure.Notifications;

public class DocumentProcessedNotificationHandler(IHubContext<NotificationsHub> _hubContext)
    : INotificationHandler<DocumentProcessedNotification>
{
    public async Task Handle(DocumentProcessedNotification notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.SendAsync("DocumentProcessed",
            new
            {
                documentId = notification.DocumentId,
                fileName = notification.FileName
            }, cancellationToken);
    }
}