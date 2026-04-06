using System.ComponentModel.DataAnnotations.Schema;

namespace PVMS.Domain.Entities
{
    public class Users : BaseEntity<Guid>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Active { get; set; }
        public int UserTypeId { get; set; }
        public string CurrentLocation { get; set; }
        public string MobileNo { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserWorkFlowDefinition> UserWorkFlowDefinitions { get; set; }
        [NotMapped]
        public string NewPassword { get; set; }
    }
}
