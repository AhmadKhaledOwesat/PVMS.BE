namespace PVMS.Domain.Entities.Filters
{
    public class WorkFlowHistoryFilter : SearchParameters<WorkFlowHistory>
    {
        public Guid TicketId { get; set; }
    }
}
