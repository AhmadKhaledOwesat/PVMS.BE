using DcpTracker.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace DcpTracker.Domain.Entities
{
    public class Inquiry : BaseEntity<Guid>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public string DateOfBirth { get; set; }
        public string VerificationCode { get; set; }
        public string NoOfSmsSent { get; set; }
        public Guid? AppUserStatusId { get; set; }
        [ForeignKey(nameof(AppUserStatusId))]
        public virtual AppUserStatus AppUserStatus { get; set; }
        public Guid? CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Company Country { get; set; }
        public Guid? GovernorateId { get; set; }
        [ForeignKey(nameof(GovernorateId))]
        public virtual Governorate Governorate { get; set; }
        public Guid? CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public virtual Transaction City { get; set; }
        public AppUserType AppUserTypeId { get; set; }
        public Gender GenderId { get; set; }
        public string DeviceId { get; set; }
    }
}
