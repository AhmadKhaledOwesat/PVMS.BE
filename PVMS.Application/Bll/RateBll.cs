using DcpTracker.Application.Interfaces;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;
using Rate = DcpTracker.Domain.Entities.Rate;

namespace DcpTracker.Application.Bll
{
    public class RateBll(IBaseDal<Rate, Guid, RateFilter> baseDal,IServiceRequestBll serviceRequestBll) : BaseBll<Rate, Guid, RateFilter>(baseDal), IRateBll
    {
        public override Task<PageResult<Rate>> GetAllAsync(RateFilter searchParameters)
        {
            if (searchParameters is not null)
                searchParameters.Expression = new Func<Rate, bool>(a => (searchParameters.Comment.IsNullOrEmpty() || a.Comment == searchParameters?.Comment) && (searchParameters.ProviderId == null || a.ProviderId == searchParameters.ProviderId));

            return base.GetAllAsync(searchParameters);
        }

        public override async Task AddAsync(Rate entity)
        {
            var request = await serviceRequestBll.GetByIdAsync(entity.RequestId.Value);
            if (request != null)
            {
                request.RequestStatusId = 24;
                request.RequestStatus = null;
                await serviceRequestBll.UpdateAsync(request);
            }
           await base.AddAsync(entity);   
        }
    }
}
