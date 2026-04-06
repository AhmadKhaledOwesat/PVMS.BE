namespace PVMS.Application.Dto
{
    public class TicketAttachmentDto : BaseDto<Guid>
    {
        public Guid TicketId { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
    }
}
