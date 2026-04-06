namespace PVMS.Application.Dto
{
    public class WorkFlowDefinitionMobileDto
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public int Active { get; set; }
        public List<Guid> TicketTypeIds { get; set; } = [];
        public List<TicketTypeLookupDto> TicketTypes { get; set; } = [];
        public List<UserWorkFlowDefinitionDto> UserWorkFlowDefinitions { get; set; } = [];
    }

    public class TicketTypeLookupDto
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public int Active { get; set; }
    }
}
