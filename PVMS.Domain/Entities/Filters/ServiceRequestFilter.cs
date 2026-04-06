namespace DcpTracker.Domain.Entities.Filters
{
    public class ServiceRequestFilter : SearchParameters<ServiceRequest>
    {
        public Guid? ClientId { get; set; }
        public bool IsProvider { get; set; }
        public Guid? ProviderId { get; set; }
    }
}
