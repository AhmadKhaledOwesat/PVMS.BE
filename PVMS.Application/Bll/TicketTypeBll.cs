using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using System.Linq.Expressions;

namespace PVMS.Application.Bll
{
    public class TicketTypeBll(IBaseDal<TicketType, Guid, TicketTypeFilter> baseDal,ITypeCategoryBll typeCategoryBll) : BaseBll<TicketType, Guid, TicketTypeFilter>(baseDal), ITicketTypeBll
    {
        public override Task<PageResult<TicketType>> GetAllAsync(TicketTypeFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!string.IsNullOrEmpty(searchParameters.Description))
                    searchParameters.Expression = new Func<TicketType, bool>(a => a.NameAr == searchParameters?.Description && (searchParameters.Active == null || a.Active == searchParameters.Active));
            }

            return base.GetAllAsync(searchParameters);
        }

        public override async Task UpdateAsync(TicketType entity)
        {
            await HandleRolePrivilages(entity);
            await base.UpdateAsync(entity);
        }

        private async Task HandleRolePrivilages(TicketType entity)
        {
            Expression<Func<TypeCategory, bool>> expression = x => x.TicketTyped == entity.Id;
            List<TypeCategory> typeCategorys = await typeCategoryBll.FindAllByExpressionAsync(expression);
            if (typeCategorys.Count > 0)
                await typeCategoryBll.DeleteRangeAsync(typeCategorys);
            foreach (var item in entity.TypeCategories)
            {
                item.TicketType = null;
                item.Id = default;
            }
            await typeCategoryBll.AddRangeAsync([.. entity.TypeCategories]);
        }
    }
}
