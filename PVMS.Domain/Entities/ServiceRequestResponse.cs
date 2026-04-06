using System.ComponentModel.DataAnnotations.Schema;

namespace DcpTracker.Domain.Entities
{
    public class ServiceRequestResponse : BaseEntity<Guid>
    {
        public Guid? ServiceRequestId { get; set; }
        [ForeignKey(nameof(ServiceRequestId))]
        public virtual ServiceRequest ServiceRequest { get; set; }
        public DateOnly ResponseDate { get; set; }
        public TimeOnly ResponseTime { get; set; }
        public Guid? ProviderId { get; set; }
        [ForeignKey(nameof(ProviderId))]
        public virtual AppUsers Provider { get; set; }
        public Guid? ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public virtual AppUsers Client { get; set; }
        public int? RequestStatusId { get; set; }
        [ForeignKey(nameof(RequestStatusId))]
        public virtual RequestStatus RequestStatus { get; set; }
    }
}
