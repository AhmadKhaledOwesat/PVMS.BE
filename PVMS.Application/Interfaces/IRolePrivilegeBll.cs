using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;

namespace PVMS.Domain.Interfaces
{
    public interface IRolePrivilegeBll : IBaseBll<RolePrivilege, Guid, RolePrivilegeFilter>
    {
    }
}
