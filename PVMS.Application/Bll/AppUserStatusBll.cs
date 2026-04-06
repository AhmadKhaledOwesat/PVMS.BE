using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;

namespace DcpTracker.Application.Bll
{
    public class AppUserStatusBll(IBaseDal<AppUserStatus,Guid, AppUserStatusFilter> baseDal) : BaseBll<AppUserStatus,Guid, AppUserStatusFilter>(baseDal), IAppUserStatusBll
    {
        public override Task<PageResult<AppUserStatus>> GetAllAsync(AppUserStatusFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!searchParameters.Name.IsNullOrEmpty())
                    searchParameters.Expression = new Func<AppUserStatus, bool>(a => a.NameAr == searchParameters?.Name && (searchParameters.Active == null || a.Active == searchParameters.Active));
            }

            return base.GetAllAsync(searchParameters);
        }
    }
}
