using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;

namespace PVMS.Application.Bll
{
    public class UserRoleBll(IBaseDal<UserRole, Guid, UserRoleFilter> baseDal) : BaseBll<UserRole, Guid, UserRoleFilter>(baseDal), IUserRoleBll
    {
        public override Task<PageResult<UserRole>> GetAllAsync(UserRoleFilter searchParameters)
        {
            searchParameters.Expression = new Func<UserRole, bool>(a => a.UserId == searchParameters.UserId);
            return base.GetAllAsync(searchParameters);
        }

    }
}
