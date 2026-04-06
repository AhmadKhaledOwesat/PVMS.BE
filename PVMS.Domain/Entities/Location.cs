namespace PVMS.Domain.Entities
{
    public class Location : BaseEntity<Guid>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public int Active { get; set; }
    }
}
