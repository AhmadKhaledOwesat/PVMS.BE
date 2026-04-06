using System.ComponentModel.DataAnnotations.Schema;

namespace PVMS.Domain.Entities
{
    public class Ticket : BaseEntity<Guid>
    {
        public string TicketNo { get; set; }
        public Guid CitizenId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? WorkFlowDefinitionId { get; set; }
        public string Coordination { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public DateTime? TicketDate { get; set; }
        public int StatusId { get; set; }
        public virtual List<TicketAttachment> TicketAttachments { get; set; } = [];
        public virtual List<WorkFlowHistory> WorkFlowHistories { get; set; } = [];
        public virtual List<TicketViolation> TicketViolations { get; set; } = [];
        [ForeignKey(nameof(CreatedBy))]
        public virtual Users Inspector { get; set; }
        [ForeignKey(nameof(CitizenId))]
        public virtual Citizen Citizen { get; set; }
        [NotMapped]
        public string BackOfficeNote { get; set; }

        [NotMapped]
        public Guid? ProcedureTypeId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; }
        [ForeignKey(nameof(WorkFlowDefinitionId))]
        public virtual WorkFlowDefinition WorkFlowDefinition { get; set; }
        public string CourtDecsion { get; set; }

        public string QrCode { get; set; }

        [NotMapped]
        public bool IsSkip { get; set; }

    }
}
