using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using PVMS.Infrastructure.Extensions;
using System.Linq.Expressions;

namespace PVMS.Application.Bll
{
    public class RoleBll(IBaseDal<Role, Guid, RoleFilter> baseDal, IRolePrivilegeBll rolePrivilegeBll, IRoleStatusBll roleStatusBll) : BaseBll<Role, Guid, RoleFilter>(baseDal), IRoleBll
    {
        public override Task<PageResult<Role>> GetAllAsync(RoleFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                searchParameters.Expression = new Func<Role, bool>(a =>
                (searchParameters.Keyword.IsNullOrEmpty() || a.NameAr.Contains(searchParameters?.Keyword)) &&
                (searchParameters.Active == null || a.Active == searchParameters.Active));
            }

            return base.GetAllAsync(searchParameters);
        }
        public override async Task UpdateAsync(Role entity)
        {
            await HandleRolePrivilages(entity);
            await HandleRoleStatusAsync(entity);
            await base.UpdateAsync(entity);
        }
        private async Task HandleRolePrivilages(Role entity)
        {
            Expression<Func<RolePrivilege, bool>> expression = x => x.RoleId == entity.Id;
            List<RolePrivilege> rolePrivileges = await rolePrivilegeBll.FindAllByExpressionAsync(expression);
            if (rolePrivileges.Count > 0)
                await rolePrivilegeBll.DeleteRangeAsync(rolePrivileges);
            foreach (var item in entity.RolePrivileges)
            {
                item.Role = null;
                item.Id = default;
            }
            await rolePrivilegeBll.AddRangeAsync([.. entity.RolePrivileges]);
        }

        private async Task HandleRoleStatusAsync(Role entity)
        {
            Expression<Func<RoleStatus, bool>> expression = x => x.RoleId == entity.Id;
            List<RoleStatus> roleStatuses = await roleStatusBll.FindAllByExpressionAsync(expression);
            if (roleStatuses.Count > 0)
                await roleStatusBll.DeleteRangeAsync(roleStatuses);
            foreach (var item in entity.RoleStatus)
            {
                item.Role = null;
                item.Id = default;
            }
            await roleStatusBll.AddRangeAsync([.. entity.RoleStatus]);
        }
    }
}
