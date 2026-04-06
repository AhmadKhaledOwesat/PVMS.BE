using System.ComponentModel.DataAnnotations.Schema;

namespace PVMS.Domain.Entities
{
    public class WorkFlowHistory : BaseEntity<Guid>
    {
        public Guid TicketId { get; set; }
        public int OldStatusId { get; set; }
        public int NewStatusId { get; set; }
        public string Note { get; set; }
        public string Title { get; set; }
        public Guid? ProcedureTypeId { get; set; }
        public virtual ProcedureType ProcedureType { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual Users User { get; set; }

        [NotMapped]
        public bool IsSkip { get; set; }

    }
}
