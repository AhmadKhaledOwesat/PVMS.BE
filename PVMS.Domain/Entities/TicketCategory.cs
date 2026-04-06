namespace PVMS.Domain.Entities
{
    public class TicketCategory : BaseEntity<Guid>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }
        public int Active { get; set; }
    }
}
