namespace PVMS.Domain.Entities.Filters
{
    public class TicketCategoryFilter : SearchParameters<TicketCategory>
    {
        public string Description { get; set; }
        public int? Active { get; set; }

    }
}
