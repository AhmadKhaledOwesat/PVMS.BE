namespace PVMS.Domain.Entities.Filters
{
    public class TicketAttachmentFilter : SearchParameters<TicketAttachment>
    {
        public Guid TicketId { get; set; }
    }
}
