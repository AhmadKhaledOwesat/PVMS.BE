namespace DcpTracker.Domain.Entities.Filters
{
    public class TutorialFilter : SearchParameters<Tutorial>
    {
        public string Term { get; set; }
        public int? Active { get; set; }

    }
}
