using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class RoleStatusBll(IBaseDal<RoleStatus, Guid, RoleStatusFilter> baseDal) : BaseBll<RoleStatus, Guid, RoleStatusFilter>(baseDal), IRoleStatusBll
    {
        public override Task<PageResult<RoleStatus>> GetAllAsync(RoleStatusFilter searchParameters)
        {
            searchParameters.Expression = new Func<RoleStatus, bool>(x => x.RoleId == searchParameters.RoleId);
            return base.GetAllAsync(searchParameters);
        }
    }
}
