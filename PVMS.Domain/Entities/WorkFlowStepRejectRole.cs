namespace PVMS.Domain.Entities
{
    public class WorkFlowStepRejectRole
    {
        public Guid Id { get; set; }
        public Guid WorkFlowStepId { get; set; }
        public Guid RoleId { get; set; }
        public virtual WorkFlowStep WorkFlowStep { get; set; }
        public virtual Role Role { get; set; }
    }
}
