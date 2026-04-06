using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;

namespace PVMS.Domain.Interfaces
{
    public interface IRoleBll : IBaseBll<Role, Guid, RoleFilter>
    {
    }
}
