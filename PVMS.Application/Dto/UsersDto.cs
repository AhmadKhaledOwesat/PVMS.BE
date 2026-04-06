using PVMS.Domain.Enum;

namespace PVMS.Application.Dto
{
    public class UsersDto : BaseDto<Guid>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string CurrentLocation { get; set; }
        public int Active { get; set; }
        public string Token { get; set; }
        public virtual ICollection<UserRoleDto> UserRoles { get; set; }
        public virtual ICollection<UserWorkFlowDefinitionDto> UserWorkFlowDefinitions { get; set; }
        public virtual Guid[] Permssions { get; set; }
        public string NewPassword { get; set; }
        public string MobileNo { get; set; }
        public UserType UserTypeId { get; set; }
        public string Otp { get; set; }

        public int[] WorkFlowStatus { get; set; } = [];
    }
}
