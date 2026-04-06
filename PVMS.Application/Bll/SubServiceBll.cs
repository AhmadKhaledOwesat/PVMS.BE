using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;

namespace DcpTracker.Application.Bll
{
    public class SubServiceBll(IBaseDal<SubService, Guid, SubServiceFilter> baseDal) : BaseBll<SubService, Guid, SubServiceFilter>(baseDal), ISubServiceBll
    {
        public override Task<PageResult<SubService>> GetAllAsync(SubServiceFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!searchParameters.Description.IsNullOrEmpty())
                    searchParameters.Expression = new Func<SubService, bool>(a => a.DescOt == searchParameters.Description);
            }

            return base.GetAllAsync(searchParameters);
        }
    }
}
