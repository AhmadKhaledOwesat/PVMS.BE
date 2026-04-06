using DcpTracker.Domain.Entities;
using DcpTracker.Domain.Entities.Filters;
using DcpTracker.Domain.Interfaces;
using DcpTracker.Infrastructure.Extensions;

namespace DcpTracker.Application.Bll
{
    public class TutorialBll(IBaseDal<Tutorial, Guid, TutorialFilter> baseDal) : BaseBll<Tutorial, Guid, TutorialFilter>(baseDal), ITutorialBll
    {
        public override Task<PageResult<Tutorial>> GetAllAsync(TutorialFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!searchParameters.Term.IsNullOrEmpty())
                    searchParameters.Expression = new Func<Tutorial, bool>(a => a.TitleAr == searchParameters?.Term && (searchParameters.Active == null || a.Active == searchParameters.Active));
            }

            return base.GetAllAsync(searchParameters);
        }
        public override async Task AddAsync(Tutorial entity)
        {
            if (!entity.Icon.IsNullOrEmpty())
                entity.Icon = await entity.Icon.SaveImage();
            await base.AddAsync(entity);
        }
        public override async Task UpdateAsync(Tutorial entity)
        {
            Tutorial tutorial = await GetByIdAsync(entity.Id);
            if (!entity.Icon.IsNullOrEmpty() && tutorial.Icon != entity.Icon)
                entity.Icon = await entity.Icon.SaveImage();
            else
                entity.Icon = tutorial.Icon;
            await base.UpdateAsync(entity);
        }
    }
}
