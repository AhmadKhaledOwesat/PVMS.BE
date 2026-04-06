namespace PVMS.Application.Dto
{
    public class CitizenDto : BaseDto<Guid>
    {
        public string FullName { get; set; }
        public string IdentityCardNumber { get; set; }
        public string NationalId { get; set; }
        public Guid? NationalityId { get; set; }
        public NationalityDto Nationality { get; set; }
        public int? GenderId { get; set; }
        public int? IsVerified { get; set; }
        public string ImagePath { get; set; }
    }
}
