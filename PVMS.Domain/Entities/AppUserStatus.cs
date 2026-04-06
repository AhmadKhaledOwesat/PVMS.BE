namespace DcpTracker.Domain.Entities
{
    public class AppUserStatus : BaseEntity<Guid>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public int LoginAllowed { get; set; }
        public int EditProfileAllowed { get; set; }
        public int RateAllowed { get; set; }
        public int AddPlacesAllowed { get; set; }
        public int Active {  get; set; }    
    }
}
