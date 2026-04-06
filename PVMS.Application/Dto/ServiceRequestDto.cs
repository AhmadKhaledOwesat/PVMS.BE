
namespace DcpTracker.Application.Dto
{
    public class ServiceRequestDto : BaseDto<Guid>
    {
        public string RequestCode { get; set; }
        public Guid? ProviderId { get; set; }
        public string ProviderName { get; set; }
        public Guid? ClientId { get; set; }
        public string ClientName { get; set; }
        public Guid? MainServiceId { get; set; }
        public string MainServiceName { get; set; }
        public DateOnly RequestDate { get; set; }
        public TimeOnly RequestTime { get; set; }
        public string RequestBriefly { get; set; }
        public string AttachmentFile { get; set; }
        public string AttachmentFileType { get; set; }
        public Guid? AddressId { get; set; }
        public string AppUserAddressLabel { get; set; }
        public int? IsPaid { get; set; }
        public decimal? ServiceAmount { get; set; }
        public int? PartsAreAvailable { get; set; }
        public int? RequestStatusId { get; set; }
        public string RequestStatusName { get; set; }
        public string RequestStatusColor { get; set; }

        public string ClientAllowedStatus { get; set; }
        public string ProviderAllowedStatus { get; set; }
       // public string ProviderGps { get; set; }
        public string ClientGps { get; set; }
        public virtual ICollection<ServiceRequestResponseDto> ServiceRequestResponses { get; set; }
    }
}
