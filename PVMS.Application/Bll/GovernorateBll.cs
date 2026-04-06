using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;

namespace DcpTracker.Application.Bll
{
    public class GovernorateBll(IBaseDal<Governorate, Guid, GovernorateFilter> baseDal) : BaseBll<Governorate, Guid, GovernorateFilter>(baseDal), IGovernorateBll
    {
        public override Task<PageResult<Governorate>> GetAllAsync(GovernorateFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!searchParameters.Description.IsNullOrEmpty())
                    searchParameters.Expression = new Func<Governorate, bool>(a => a.DescOt == searchParameters?.Description && (searchParameters.Active == null || a.Active == searchParameters.Active));
            }

            return base.GetAllAsync(searchParameters);
        }
    }
}
