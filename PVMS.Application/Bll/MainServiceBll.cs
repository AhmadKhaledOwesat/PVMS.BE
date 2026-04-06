using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;

namespace DcpTracker.Application.Bll
{
    public class MainServiceBll(IBaseDal<MainService, Guid, MainServiceFilter> baseDal) : BaseBll<MainService, Guid, MainServiceFilter>(baseDal), IMainServiceBll
    {
        public override async Task AddAsync(MainService entity)
        {
                if (!entity.Icon.IsNullOrEmpty())
                    entity.Icon = await entity.Icon.SaveImage();
            await base.AddAsync(entity);
        }
        public override async Task UpdateAsync(MainService entity)
        {
            MainService mainService = await GetByIdAsync(entity.Id);
            if (!entity.Icon.IsNullOrEmpty() && mainService.Icon != entity.Icon)
                entity.Icon = await entity.Icon.SaveImage();
            else
                entity.Icon = mainService.Icon;
            await base.UpdateAsync(entity);
        }
        public override Task<PageResult<MainService>> GetAllAsync(MainServiceFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!searchParameters.Description.IsNullOrEmpty())
                    searchParameters.Expression = new Func<MainService, bool>(a => a.DescOt == searchParameters?.Description  
                    && (searchParameters.ActiveForDisplay == null || a.ActiveForAppDisplay == searchParameters.ActiveForDisplay)
                    && (searchParameters.ActiveForAdd == null || a.ActiveForUserAdd == searchParameters.ActiveForAdd));
            }

            return base.GetAllAsync(searchParameters);
        }
    }
}
