namespace PVMS.Domain.Entities
{
    public class TicketTypePrice : BaseEntity<Guid>
    {
        public Guid TicketTypeId { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal Amount { get; set; }
    }
}
