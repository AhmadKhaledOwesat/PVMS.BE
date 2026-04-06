using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class NationalityBll(IBaseDal<Nationality, Guid, NationalityFilter> baseDal) : BaseBll<Nationality, Guid, NationalityFilter>(baseDal), INationalityBll
    {
        public override Task<PageResult<Nationality>> GetAllAsync(NationalityFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!string.IsNullOrEmpty(searchParameters.Description))
                    searchParameters.Expression = new Func<Nationality, bool>(a => a.NameAr == searchParameters?.Description && (searchParameters.Active == null || a.Active == searchParameters.Active));
            }

            return base.GetAllAsync(searchParameters);
        }

        public override Task UpdateAsync(Nationality entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}
