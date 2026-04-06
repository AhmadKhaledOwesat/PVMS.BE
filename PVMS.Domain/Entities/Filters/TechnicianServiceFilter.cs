namespace DcpTracker.Domain.Entities.Filters
{
    public class TechnicianServiceFilter : SearchParameters<TechnicianService>
    {
        public string Name { get; set; }
    }
}
