using System.ComponentModel.DataAnnotations.Schema;

namespace PVMS.Domain.Entities
{
    public class UserRole : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual Users User { get; set; }

        public Guid RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }  
    }
}
