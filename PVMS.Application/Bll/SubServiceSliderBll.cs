using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;

namespace DcpTracker.Application.Bll
{
    public class SubServiceSliderBll(IBaseDal<SubServiceSlider, Guid, SubServiceSliderFilter> baseDal) : BaseBll<SubServiceSlider, Guid, SubServiceSliderFilter>(baseDal), ISubServiceSliderBll
    {
        public override Task<PageResult<SubServiceSlider>> GetAllAsync(SubServiceSliderFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!searchParameters.Description.IsNullOrEmpty())
                    searchParameters.Expression = new Func<SubServiceSlider, bool>(a => a.CallActionTextAr == searchParameters?.Description);
            }

            return base.GetAllAsync(searchParameters);
        }
    }
}
