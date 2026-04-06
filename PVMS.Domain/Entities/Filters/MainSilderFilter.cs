namespace DcpTracker.Domain.Entities.Filters
{
    public class MainSliderFilter : SearchParameters<MainSlider>
    {
        public string Description { get; set; }
        public int? Active { get; set; }

    }
}
