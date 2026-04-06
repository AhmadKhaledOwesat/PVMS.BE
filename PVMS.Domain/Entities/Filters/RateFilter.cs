namespace DcpTracker.Domain.Entities.Filters
{
    public class RateFilter : SearchParameters<Rate>
    {
        public string Comment { get; set; }
        public Guid? ProviderId { get; set; }
    }
}
