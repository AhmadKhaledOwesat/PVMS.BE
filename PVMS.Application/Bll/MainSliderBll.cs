using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;

namespace DcpTracker.Application.Bll
{
    public class MainSliderBll(IBaseDal<MainSlider, Guid, MainSliderFilter> baseDal) : BaseBll<MainSlider, Guid, MainSliderFilter>(baseDal), IMainSliderBll
    {
        public override async Task AddAsync(MainSlider entity)
        {
            if (!entity.ImageFile.IsNullOrEmpty())
                entity.ImageFile = await entity.ImageFile.SaveImage();
            await base.AddAsync(entity);
        }
        public override async Task UpdateAsync(MainSlider entity)
        {
            MainSlider mainSlider = await GetByIdAsync(entity.Id);
            if (!entity.ImageFile.IsNullOrEmpty() && mainSlider.ImageFile != entity.ImageFile)
                entity.ImageFile = await entity.ImageFile.SaveImage();
            else
                entity.ImageFile = mainSlider.ImageFile;
            await base.UpdateAsync(entity);
        }
        public override Task<PageResult<MainSlider>> GetAllAsync(MainSliderFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!searchParameters.Description.IsNullOrEmpty())
                    searchParameters.Expression = new Func<MainSlider, bool>(a => a.CallActionTextAr == searchParameters?.Description && (searchParameters.Active == null || a.Active == searchParameters.Active));
            }

            return base.GetAllAsync(searchParameters);
        }
    }
}
