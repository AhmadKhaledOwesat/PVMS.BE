
namespace PVMS.Application.Dto
{
    public class PrivilegeDto : BaseDto<Guid>
    {
        public string PrivilegeName { get; set; }
        public string PrivilegeNameEn { get; set; }
        public Guid? ParentId { get; set; }
        public string Icon { get; set; }
        public string ParentName { get; set; }
        public int? SortOrder { get; set; }

        public virtual ICollection<RolePrivilegeDto> RolePrivileges { get; set; }

    }
}
