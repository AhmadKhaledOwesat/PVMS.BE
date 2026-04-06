using System.ComponentModel.DataAnnotations.Schema;

namespace DcpTracker.Domain.Entities
{
    public class ServiceRequest : BaseEntity<Guid>
    {
        public string RequestCode { get; set; }
        public Guid? ProviderId { get; set; }
        [ForeignKey(nameof(ProviderId))]
        public virtual AppUsers Provider { get; set; }
        public Guid? ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public virtual AppUsers Client { get; set; }
        public Guid? MainServiceId { get; set; }
        public Guid? SubServiceId { get; set; }
        [ForeignKey(nameof(MainServiceId))]
        public virtual MainService MainService { get; set; }
        public DateOnly RequestDate { get; set; }
        public TimeOnly RequestTime { get; set; }
        public string RequestBriefly { get; set; }
        public string AttachmentFile { get; set; }
        public string AttachmentFileType { get; set; }
        public Guid? AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual AppUserAddress AppUserAddress { get; set; }
        public int? RequestStatusId { get; set; }
        [ForeignKey(nameof(RequestStatusId))]
        public virtual RequestStatus RequestStatus { get; set; }
        public int? IsPaid { get; set; }
        public int? PartsAreAvailable { get; set; }
        public decimal? ServiceAmount { get; set; }
        public virtual ICollection<ServiceRequestResponse> ServiceRequestResponses { get; set; }
       // public string ProviderGps { get; set; }
       // public string ClientGps { get; set; }
    }
}
