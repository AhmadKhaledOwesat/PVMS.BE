namespace DcpTracker.Application.Dto
{
    public class RequestStatusDto : BaseDto<int>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public string LabelAr { get; set; }
        public string LabelOt { get; set; }
        public string Color { get; set; }
        public int Active { get; set; }
        public string ClientAllowedStatus { get; set; }
        public string ProviderAllowedStatus{ get; set; }
        public int NotifyClient { get; set; }
        public int NotifyProvider { get; set; }
    }
}
