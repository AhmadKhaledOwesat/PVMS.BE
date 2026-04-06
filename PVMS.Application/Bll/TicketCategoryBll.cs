using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class TicketCategoryBll(IBaseDal<TicketCategory, Guid, TicketCategoryFilter> baseDal) : BaseBll<TicketCategory, Guid, TicketCategoryFilter>(baseDal), ITicketCategoryBll
    {
        public override Task<PageResult<TicketCategory>> GetAllAsync(TicketCategoryFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!string.IsNullOrEmpty(searchParameters.Description))
                    searchParameters.Expression = new Func<TicketCategory, bool>(a => a.NameAr == searchParameters?.Description && (searchParameters.Active == null || a.Active == searchParameters.Active));
            }

            return base.GetAllAsync(searchParameters);
        }
    }
}
