using DcpTracker.Domain.Enum;

namespace DcpTracker.Application.Dto
{
    public class InquiryDto : BaseDto<Guid>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public string DateOfBirth { get; set; }
        public string VerificationCode { get; set; }
        public string NoOfSmsSent { get; set; }
        public Guid? GovernorateId { get; set; }
        public string GovernorateName { get; set; }
        public Guid? AppUserStatusId { get; set; }
        public string AppUserStatusName { get; set; }
        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
        public Guid? CityId { get; set; }
        public string CityName { get; set; }
        public AppUserType AppUserTypeId { get; set; }
        public Gender GenderId { get; set; }
        public string DeviceId { get; set; }
    }
}
