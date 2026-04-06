using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Enum;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Interfaces
{
    public interface INotificationBll : IBaseBll<Notifications, Guid, NotificationFilter>
    {
        Task AddRangeAsync(List<Notifications> entities, TicketStatus ticketStatus);
        Task<bool> ReadNotificationAsync(Guid id);
    }
}
