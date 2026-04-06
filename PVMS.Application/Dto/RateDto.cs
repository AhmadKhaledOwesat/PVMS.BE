namespace DcpTracker.Application.Dto
{
    public class RateDto : BaseDto<Guid>
    {
        public Guid? RequestId { get; set; }
        public string RequestCode { get; set; }
        public string ServiceName { get; set; }
        public Guid? ProviderId { get; set; }
        public string ProviderName { get; set; }
        public Guid? ClientId { get; set; }
        public string ClientName { get; set; }
        public string Comment { get; set; }
        public int Star { get; set; }
    }
}
