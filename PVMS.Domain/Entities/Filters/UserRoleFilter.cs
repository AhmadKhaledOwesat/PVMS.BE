namespace PVMS.Domain.Entities.Filters
{
    public class UserRoleFilter : SearchParameters<UserRole>
    {
        public Guid UserId { get; set; }
    }
}
