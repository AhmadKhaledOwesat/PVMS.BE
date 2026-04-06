namespace DcpTracker.Application.Dto
{
    public class TechnicianServiceDto : BaseDto<Guid>
    {
        public Guid? ProviderId { get; set; }
        public string ProviderName { get; set; }
        public Guid? MainServiceId { get; set; }
        public string MainServiceName { get; set; }
    }
}
