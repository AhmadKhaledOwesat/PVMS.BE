namespace DcpTracker.Domain.Entities.Filters
{
    public class MainServiceFilter : SearchParameters<MainService>
    {
        public string Description { get; set; }
        public int? ActiveForAdd { get; set; }
        public int? ActiveForDisplay { get; set; }

    }
}
