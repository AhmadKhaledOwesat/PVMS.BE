namespace PVMS.Domain.Entities.Filters
{
    public class NotificationFilter : SearchParameters<Notifications>
    {
            public Guid UserId { get; set; }
        public int IsRead { get; set; }
    }
}
