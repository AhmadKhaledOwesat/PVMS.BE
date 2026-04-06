namespace PVMS.Domain.Entities.Filters
{
    public class LocationFilter : SearchParameters<Location>
    {
        public string Description { get; set; }
        public int? Active { get; set; }

    }
}
