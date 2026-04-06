using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class TicketViolationBll(IBaseDal<TicketViolation, Guid, TicketViolationFilter> baseDal) : BaseBll<TicketViolation, Guid, TicketViolationFilter>(baseDal), ITicketViolationBll
    {
        public override Task<PageResult<TicketViolation>> GetAllAsync(TicketViolationFilter searchParameters)
        {
            return base.GetAllAsync(searchParameters);
        }

    }
}
