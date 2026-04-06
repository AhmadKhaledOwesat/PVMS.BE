using System.ComponentModel.DataAnnotations.Schema;

namespace DcpTracker.Domain.Entities
{
    public class Rate : BaseEntity<Guid>
    {
        public Guid? RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]
        public virtual ServiceRequest ServiceRequest { get; set; }

        public Guid? ProviderId { get; set; }
        [ForeignKey(nameof(ProviderId))]
        public virtual AppUsers Provider { get; set; }

        public Guid? ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public virtual AppUsers Client { get; set; }

        public string Comment { get; set; }
        public int Star { get; set; }   
    }
}
