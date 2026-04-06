namespace PVMS.Domain.Entities
{
    public class Role : BaseEntity<Guid>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public int Active { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
        public virtual ICollection<RoleStatus> RoleStatus { get; set; }

    }
}
