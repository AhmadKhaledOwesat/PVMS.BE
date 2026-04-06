namespace PVMS.Application.Dto
{
    public class TicketViolationDto : BaseDto<Guid>
    {
        public Guid TicketId { get; set; }
        public Guid TicketTypeId { get; set; }
        public TicketTypeDto TicketType { get; set; }
        public decimal? Amount { get; set; }

    }
}
