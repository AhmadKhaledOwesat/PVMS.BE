namespace PVMS.Application.Dto
{
    public class UserRoleDto : BaseDto<Guid>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
