namespace PVMS.Domain.Entities
{
    public class TicketType : BaseEntity<Guid>
    {
        public string NameAr { get; set; }
        public string NameOt { get; set; }

        public int Active { get; set; }
        public string Notes { get; set; }
        public virtual List<TicketTypePrice> TicketTypePrices { get; set; } = [];
        public virtual ICollection<TypeCategory> TypeCategories { get; set; }

    }
}
