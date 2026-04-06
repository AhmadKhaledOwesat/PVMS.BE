using PVMS.Domain.Entities;

namespace PVMS.Application.Dto
{
    public class TicketDto : BaseDto<Guid>
    {
        public string TicketNo { get; set; }
        public Guid CitizenId { get; set; }
        public string Coordination { get; set; }
        public string Address { get; set; }
        public DateTime? TicketDate { get; set; }
        public string Note { get; set; }
        public int StatusId { get; set; }
        public Guid? WorkFlowDefinitionId { get; set; }
        public List<TicketAttachmentDto> TicketAttachments { get; set; } = [];
        public List<TicketViolationDto> TicketViolations { get; set; } = [];
        public  List<WorkFlowHistoryDto> WorkFlowHistories { get; set; } = [];
        public virtual UsersDto Inspector { get; set; }
        public virtual CitizenDto Citizen { get; set; }
        public virtual LocationDto Location { get; set; }
        public string BackOfficeNote { get; set; }
        public Guid? ProcedureTypeId { get; set; }
        public Guid? LocationId { get; set; }
        public string QrCode { get; set; }
        public bool IsSkip { get; set; }

    }
}
