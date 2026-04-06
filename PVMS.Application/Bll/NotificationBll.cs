using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Enum;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class NotificationBll(IBaseDal<Notifications, Guid, NotificationFilter> baseDal, INotificationService notificationService) : BaseBll<Notifications, Guid, NotificationFilter>(baseDal), INotificationBll
    {
        public override async Task<PageResult<Notifications>> GetAllAsync(NotificationFilter searchParameters)
        {
            searchParameters.Expression = new Func<Notifications, bool>(a => a.IsRead == searchParameters.IsRead && a.UserId == searchParameters.UserId);
            var data =  await base.GetAllAsync(searchParameters);
            data.Collections = [.. data.Collections.OrderByDescending(a => a.CreatedDate)];
            return data;
        }
        public override async Task AddAsync(Notifications entity)
        {
            await base.AddAsync(entity);
        }
        public async Task<bool> ReadNotificationAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            entity.IsRead = 1;
            await UpdateAsync(entity);
            return true;
        }
        public async Task AddRangeAsync(List<Notifications> entities, TicketStatus ticketStatus)
        {
            foreach (Notifications entity in entities)
            {
                entity.CreatedDate = DateTime.Now;
                entity.Body = BuildContent(ticketStatus, entity);
            }
            await base.AddRangeAsync(entities);
        }
        private static string BuildContent(TicketStatus ticketStatus, Notifications notifications)
        {
            switch (ticketStatus)
            {
                case TicketStatus.UnderReview:
                    return $"تم استلام ضبط رقم {notifications.Body} من الميدان بحاجة الى مراجعة من قبل المشرف";
                case TicketStatus.UnderVerification:
                    return $"تم استلام ضبط رقم {notifications.Body} من المشرف بحاجة الى مراجعة من قبل العمليات والسيطرة";
                case TicketStatus.LegalApproval:
                    return $"تم استلام ضبط رقم {notifications.Body} من العمليات والسيطرة بحاجة الى مراجعة من قبل الدائرة القانونية";
                case TicketStatus.SentToCourt:
                    return $"تم استلام ضبط رقم {notifications.Body} من الجهة القانونية بحاجة للتحويل الى المحكمة";
                case TicketStatus.Archived:
                    return $"تم استلام الضبط رقم {notifications.Body} من المحكمة بحاجة للأرشفة";
                default:
                    break;
            }
            return notifications.Body;
        }
    }
}
