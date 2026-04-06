namespace DcpTracker.Domain.Entities.Filters
{
    public class AppUserStatusFilter : SearchParameters<AppUserStatus>
    {
        public string Name { get; set; }
        public int?  Active { get; set; }
    }
}
