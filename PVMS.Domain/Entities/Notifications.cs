namespace PVMS.Domain.Entities
{
    public class Notifications : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public Guid EntityId { get; set; }
        public int IsRead { get; set; }
    }
}
