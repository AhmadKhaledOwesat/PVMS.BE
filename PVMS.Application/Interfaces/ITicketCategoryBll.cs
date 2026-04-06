using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Interfaces
{
    public interface ITicketCategoryBll : IBaseBll<TicketCategory, Guid, TicketCategoryFilter>
    {
    }
}
