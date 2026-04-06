namespace PVMS.Domain.Entities.Filters
{
    public class TaskTypePriceFilter : SearchParameters<TicketTypePrice>
    {
        public Guid TicketTypeId { get; set; }

    }
}
