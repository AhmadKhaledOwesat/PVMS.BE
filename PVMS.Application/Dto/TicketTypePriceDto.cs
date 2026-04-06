namespace PVMS.Application.Dto
{
    public class TicketTypePriceDto : BaseDto<Guid>
    {
        public Guid TicketTypeId { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal Amount { get; set; }
    }
}
