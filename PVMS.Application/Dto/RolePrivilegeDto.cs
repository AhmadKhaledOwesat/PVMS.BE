namespace PVMS.Application.Dto
{
    public class RolePrivilegeDto : BaseDto<Guid>
    {
        public Guid PrivilegeId { get; set; }
        public string PrivilegeName { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
