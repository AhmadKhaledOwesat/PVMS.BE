namespace PVMS.Domain.Entities
{
    public class WorkFlowStep : BaseEntity<Guid>
    {
        public Guid WorkFlowDefinitionId { get; set; }
        public virtual WorkFlowDefinition WorkFlowDefinition { get; set; }

        /// <summary>1-based sequence after save.</summary>
        public int StepOrder { get; set; }

        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public bool RequireNote { get; set; }
        public string NotePrompt { get; set; }

        public virtual ICollection<WorkFlowStepApproveRole> ApproveRoles { get; set; } = new List<WorkFlowStepApproveRole>();
        public virtual ICollection<WorkFlowStepRejectRole> RejectRoles { get; set; } = new List<WorkFlowStepRejectRole>();
        public virtual ICollection<WorkFlowStepSkipRole> SkipRoles { get; set; } = new List<WorkFlowStepSkipRole>();
    }
}
