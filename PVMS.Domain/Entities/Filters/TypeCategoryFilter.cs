namespace PVMS.Domain.Entities.Filters
{
    public class TypeCategoryFilter : SearchParameters<TypeCategory>
    {
        public Guid TicketTypeId { get; set; }
    }
}
