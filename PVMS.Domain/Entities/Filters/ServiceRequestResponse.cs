namespace DcpTracker.Domain.Entities.Filters
{
    public class ServiceRequestResponseFilter : SearchParameters<ServiceRequestResponse>
    {
        public string Name {  get; set; }   
    }
}
