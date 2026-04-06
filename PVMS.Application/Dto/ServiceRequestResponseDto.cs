namespace DcpTracker.Application.Dto
{
    public class ServiceRequestResponseDto : BaseDto<Guid>
    {
        public Guid? ServiceRequestId { get; set; }
        public DateOnly ResponseDate { get; set; }
        public TimeOnly ResponseTime { get; set; }
        public Guid? ProviderId { get; set; }
        public string ProviderName { get; set; }
        public Guid? ClientId { get; set; }
        public string ClientName { get; set; }
        public int? RequestStatusId { get; set; }
        public string RequestStatusName { get; set; }
    }
}
