namespace DcpTracker.Domain.Entities.Filters
{
    public class AppUserAddressFilter : SearchParameters<AppUserAddress>
    {
        public string AddressLabel { get; set; }
    }
}
