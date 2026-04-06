namespace PVMS.Domain.Entities.Filters
{
    public class TicketViolationFilter : SearchParameters<TicketViolation>
    {
        public Guid TicketId { get; set; }
    }
}
