namespace PVMS.Domain.Entities.Filters
{
    public class TicketFilter : SearchParameters<Ticket>
    {
        public string TicketNo { get; set; }
        public int? StatusId { get; set; }
        public Guid? InspectorId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Active { get; set; }
    }
}
