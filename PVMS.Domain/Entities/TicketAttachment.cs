namespace PVMS.Domain.Entities
{
    public class TicketAttachment : BaseEntity<Guid>
    {
        public Guid TicketId { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
    }
}
