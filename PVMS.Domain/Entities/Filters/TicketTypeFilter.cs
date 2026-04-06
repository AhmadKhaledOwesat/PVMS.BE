namespace PVMS.Domain.Entities.Filters
{
    public class TicketTypeFilter : SearchParameters<TicketType>
    {
        public string Description { get; set; }
        public int? Active { get; set; }

    }
}
