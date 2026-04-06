using System.ComponentModel.DataAnnotations.Schema;

namespace DcpTracker.Domain.Entities
{
    public class TechnicianService : BaseEntity<Guid>
    {
        public Guid? ProviderId { get; set; }
        [ForeignKey(nameof(ProviderId))]
        public virtual AppUsers Provider { get; set; }
        public Guid? MainServiceId { get; set; }
        [ForeignKey(nameof(MainServiceId))]
        public virtual MainService MainService { get; set; }
    }
}
