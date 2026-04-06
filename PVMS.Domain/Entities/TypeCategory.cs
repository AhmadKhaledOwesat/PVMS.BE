using System.ComponentModel.DataAnnotations.Schema;

namespace PVMS.Domain.Entities
{
    public class TypeCategory : BaseEntity<Guid>
    {
        public Guid TicketTyped { get; set; }
        [ForeignKey(nameof(TicketTyped))]
        public virtual TicketType TicketType { get; set; }

        public Guid TicketCategoryId { get; set; }
        [ForeignKey(nameof(TicketCategoryId))]
        public virtual TicketCategory TicketCategory { get; set; }  
    }
}
