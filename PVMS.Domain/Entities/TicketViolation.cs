using System.ComponentModel.DataAnnotations.Schema;

namespace PVMS.Domain.Entities
{
    public class TicketViolation : BaseEntity<Guid>
    {
        public Guid TicketId { get; set; }
        public Guid TicketTypeId { get; set; }

        [ForeignKey(nameof(TicketTypeId))]
        public virtual TicketType TicketType { get; set; }
        public decimal? Amount { get; set; }
    }
}
