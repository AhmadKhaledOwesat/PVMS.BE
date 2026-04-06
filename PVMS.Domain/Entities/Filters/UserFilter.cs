using PVMS.Domain.Enum;

namespace PVMS.Domain.Entities.Filters
{
    public class UserFilter : SearchParameters<Users>
    {
        public string UserName { get; set; }
        public int? UserTypeId { get; set; }
        public int? Active { get; set; }
        public Guid? RoleId { get; set; }
    }
}
