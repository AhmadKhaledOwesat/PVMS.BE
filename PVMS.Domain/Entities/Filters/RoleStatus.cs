namespace PVMS.Domain.Entities.Filters
{
    public class RoleStatusFilter : SearchParameters<RoleStatus>
    {
        public Guid RoleId { get; set; }
    }
}
