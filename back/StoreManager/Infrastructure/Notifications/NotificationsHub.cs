using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace StoreManager.Infrastructure.Notifications;

[Authorize]
public class NotificationsHub : Hub
{
    
}