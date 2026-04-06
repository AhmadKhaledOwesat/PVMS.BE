using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class LocationBll(IBaseDal<Location, Guid, LocationFilter> baseDal) : BaseBll<Location, Guid, LocationFilter>(baseDal), ILocationBll
    {
        public override Task<PageResult<Location>> GetAllAsync(LocationFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!string.IsNullOrEmpty(searchParameters.Description))
                    searchParameters.Expression = new Func<Location, bool>(a => a.NameAr == searchParameters?.Description && (searchParameters.Active == null || a.Active == searchParameters.Active));
            }

            return base.GetAllAsync(searchParameters);
        }

        public override Task UpdateAsync(Location entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}
