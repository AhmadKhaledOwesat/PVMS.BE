using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;

namespace DcpTracker.Application.Bll
{
    public class RequestStatusBll(IBaseDal<RequestStatus, int, RequestStatusFilter> baseDal) : BaseBll<RequestStatus, int, RequestStatusFilter>(baseDal), IRequestStatusBll
    {
        public override Task<PageResult<RequestStatus>> GetAllAsync(RequestStatusFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!searchParameters.Name.IsNullOrEmpty())
                    searchParameters.Expression = new Func<RequestStatus, bool>(a => a.NameAr == searchParameters?.Name);
            }

            return base.GetAllAsync(searchParameters);
        }
    }
}
