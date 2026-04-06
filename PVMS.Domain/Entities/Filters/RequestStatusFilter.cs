namespace DcpTracker.Domain.Entities.Filters
{
    public class RequestStatusFilter : SearchParameters<RequestStatus>
    {
        public string Name { get; set; }
        public int? Active { get; set; }
    }
}
