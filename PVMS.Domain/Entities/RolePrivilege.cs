using System.ComponentModel.DataAnnotations.Schema;

namespace PVMS.Domain.Entities
{
    public class RolePrivilege : BaseEntity<Guid>
    {
        public Guid PrivilegeId { get; set; }
        [ForeignKey(nameof(PrivilegeId))]
        public virtual Privilege Privilege { get; set; }

        public Guid RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }  
    }
}
