
using PVMS.Domain.Entities;

namespace PVMS.Application.Dto
{
    public class RoleDto : BaseDto<Guid>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public int Active { get; set; }
        
        public virtual ICollection<UserRoleDto> UserRoles { get; set; }
        public virtual ICollection<RolePrivilegeDto> RolePrivileges { get; set; }
        public virtual ICollection<RoleStatusDto> RoleStatus { get; set; }

    }
}
