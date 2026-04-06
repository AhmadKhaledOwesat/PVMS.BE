namespace PVMS.Domain.Entities
{
    public class WorkFlowDefinitionTicketType
    {
        public Guid Id { get; set; }
        public Guid WorkFlowDefinitionId { get; set; }
        public Guid TicketTypeId { get; set; }
        public virtual WorkFlowDefinition WorkFlowDefinition { get; set; }
        public virtual TicketType TicketType { get; set; }
    }
}
