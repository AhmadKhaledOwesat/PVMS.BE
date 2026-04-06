using System.ComponentModel.DataAnnotations.Schema;

namespace PVMS.Domain.Entities
{
    public class UserWorkFlowDefinition : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual Users User { get; set; }

        public Guid WorkFlowDefinitionId { get; set; }
        [ForeignKey(nameof(WorkFlowDefinitionId))]
        public virtual WorkFlowDefinition WorkFlowDefinition { get; set; }
    }
}
