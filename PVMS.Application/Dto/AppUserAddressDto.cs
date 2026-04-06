namespace DcpTracker.Application.Dto
{
    public class AppUserAddressDto : BaseDto<Guid>
    {
        public string AddressLabel { get; set; }
        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
        public Guid? GovernorateId { get; set; }
        public string GovernorateName { get; set; }
        public Guid? CityId { get; set; }
        public string CityName { get; set; }
        public string Street { get; set; }
        public string BuildingName { get; set; }
        public string Floor { get; set; }
        public string AptNumber { get; set; }
        public string MapLocation { get; set; }
        public Guid AppUserId { get; set; }
        public string AppUserName { get; set; }
        public int Active { get; set; }
    }
}
