using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class TicketTypePriceBll(IBaseDal<TicketTypePrice, Guid, TaskTypePriceFilter> baseDal) : BaseBll<TicketTypePrice, Guid, TaskTypePriceFilter>(baseDal), ITicketTypePriceBll
    {
        public override Task<PageResult<TicketTypePrice>> GetAllAsync(TaskTypePriceFilter searchParameters)
        {
            searchParameters.Expression = new Func<TicketTypePrice, bool>(a => a.TicketTypeId == searchParameters?.TicketTypeId);
            return base.GetAllAsync(searchParameters);
        }
    }
}
