using Microsoft.AspNetCore.SignalR;
using PVMS.Application.Hubs;
using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;

namespace PVMS.Application.Services
{
    public class NotificationService(IHubContext<NotificationHub> hub) : INotificationService
    {
        public async Task SendAsync(Guid userId, Notifications notification)
        {
            await hub.Clients
                .Group(userId.ToString())
                .SendAsync("ReceiveNotification", notification);
        }
    }
}
