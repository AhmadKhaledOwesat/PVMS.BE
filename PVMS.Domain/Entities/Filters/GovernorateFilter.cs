namespace DcpTracker.Domain.Entities.Filters
{
    public class GovernorateFilter : SearchParameters<Governorate>
    {
        public string Description { get; set; }
        public int? Active { get; set; }

    }
}
