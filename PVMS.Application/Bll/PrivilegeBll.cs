using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using PVMS.Infrastructure.Extensions;

namespace PVMS.Application.Bll
{
    public class PrivilegeBll(IBaseDal<Privilege, Guid, PrivilegeFilter> baseDal) : BaseBll<Privilege, Guid, PrivilegeFilter>(baseDal), IPrivilegeBll
    {
        public override async Task<PageResult<Privilege>> GetAllAsync(PrivilegeFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                if (!string.IsNullOrEmpty(searchParameters.Name))
                    searchParameters.Expression = new Func<Privilege, bool>(a => (searchParameters.Keyword.IsNullOrEmpty() || a.PrivilegeName.Contains(searchParameters?.Keyword)));
            }

            var data = await base.GetAllAsync(searchParameters);
            data.Collections = [.. data.Collections.OrderBy(a => a.SortOrder)];
            return data;
        }
    }
}
