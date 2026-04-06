using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class TypeCategoryBll(IBaseDal<TypeCategory, Guid, TypeCategoryFilter> baseDal) : BaseBll<TypeCategory, Guid, TypeCategoryFilter>(baseDal), ITypeCategoryBll
    {
        public override Task<PageResult<TypeCategory>> GetAllAsync(TypeCategoryFilter searchParameters)
        {
            searchParameters.Expression = new Func<TypeCategory, bool>(a => a.TicketTyped == searchParameters.TicketTypeId);
            return base.GetAllAsync(searchParameters);
        }

    }
}
