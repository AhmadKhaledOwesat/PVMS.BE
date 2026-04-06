namespace PVMS.Domain.Entities
{
    public class WorkFlowDefinition : BaseEntity<Guid>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public int Active { get; set; }
        public virtual ICollection<WorkFlowDefinitionTicketType> TicketTypes { get; set; } = new List<WorkFlowDefinitionTicketType>();
        public virtual ICollection<UserWorkFlowDefinition> UserWorkFlowDefinitions { get; set; } = new List<UserWorkFlowDefinition>();
        public virtual ICollection<WorkFlowStep> Steps { get; set; } = new List<WorkFlowStep>();
    }
}
