namespace PVMS.Application.Dto
{
    public class WorkFlowDefinitionDto : BaseDto<Guid>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public List<Guid> TicketTypeIds { get; set; } = [];
        public int Active { get; set; }
        public List<WorkFlowStepDto> Steps { get; set; } = [];
    }
}
