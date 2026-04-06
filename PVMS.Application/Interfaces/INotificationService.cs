using PVMS.Application.Dto;
using PVMS.Domain.Entities;

namespace PVMS.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Guid userId, Notifications notification);
    }
}
