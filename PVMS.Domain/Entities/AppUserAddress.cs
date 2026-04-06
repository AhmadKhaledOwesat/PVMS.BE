using System.ComponentModel.DataAnnotations.Schema;

namespace DcpTracker.Domain.Entities
{
    public class AppUserAddress : BaseEntity<Guid>
    {
        public string AddressLabel { get; set; }
        public Guid? CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Company Country { get; set; }
        public Guid? GovernorateId { get; set; }
        [ForeignKey(nameof(GovernorateId))]
        public virtual Governorate Governorate { get; set; }
        public Guid? CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public virtual Transaction City { get; set; }
        public string Street { get; set; }
        public string BuildingName { get; set; }
        public string Floor { get; set; }
        public string AptNumber { get; set; }
        public string MapLocation { get; set; }
        public Guid AppUserId { get; set; }
        [ForeignKey(nameof(AppUserId))]
        public virtual AppUsers AppUser { get; set; }
        public int Active { get; set; }
    }
}
