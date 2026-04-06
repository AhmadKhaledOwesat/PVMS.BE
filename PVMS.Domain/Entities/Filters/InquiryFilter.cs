namespace DcpTracker.Domain.Entities.Filters
{
    public class InquiryFilter : SearchParameters<Inquiry>
    {
        public string FullName { get; set; }
    }
}
