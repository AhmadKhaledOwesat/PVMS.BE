using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;

namespace DcpTracker.Application.Bll
{
    public class AppUserAddressBll(IBaseDal<AppUserAddress, Guid, AppUserAddressFilter> baseDal) : BaseBll<AppUserAddress, Guid, AppUserAddressFilter>(baseDal), IAppUserAddressBll
    {
        public override Task<PageResult<AppUserAddress>> GetAllAsync(AppUserAddressFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!searchParameters.AddressLabel.IsNullOrEmpty())
                    searchParameters.Expression = new Func<AppUserAddress, bool>(a => a.AddressLabel == searchParameters?.AddressLabel);
            }

            return base.GetAllAsync(searchParameters);
        }
    }
}
