namespace DcpTracker.Domain.Entities.Filters
{
    public class FaqFilter : SearchParameters<Faq>
    {
        public string Term { get; set; }
        public int? Active { get; set; }
    }
}
