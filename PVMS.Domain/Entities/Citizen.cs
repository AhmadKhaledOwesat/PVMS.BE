namespace PVMS.Domain.Entities
{
    public class Citizen : BaseEntity<Guid>
    {
        public string FullName { get; set; }
        public string IdentityCardNumber { get; set; }
        public string NationalId { get; set; }
        public Guid? NationalityId { get; set; }
        public virtual Nationality Nationality { get; set; }
        public int? GenderId { get; set; }
        public int? IsVerified { get; set; }
        public string ImagePath { get; set; }
    }
}
