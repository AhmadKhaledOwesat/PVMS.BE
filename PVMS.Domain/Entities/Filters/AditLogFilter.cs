namespace PVMS.Domain.Entities.Filters
{
    public class AditLogFilter
    {
        public PagingParameters? PagingParameters { get; set; }
        public string? EntityType { get; set; }
        public Guid? EntityId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
    }
}
