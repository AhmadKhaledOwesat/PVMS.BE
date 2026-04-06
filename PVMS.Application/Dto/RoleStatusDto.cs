namespace PVMS.Application.Dto
{
    public class RoleStatusDto : BaseDto<Guid>
    {
        public int StatusId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
