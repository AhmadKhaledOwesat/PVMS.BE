namespace DcpTracker.Domain.Entities.Filters
{
    public class SubServiceFilter : SearchParameters<SubService>
    {
        public string Description { get; set; }
    }
}
